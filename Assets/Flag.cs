using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Team Team;

    private Vector3 _original_location;
    private Vector3 _drift_location;
    private Quaternion _original_rotation;
    private Quaternion _drift_rotation;
    private float _drift_seconds;
    private float _drift_timer = 0;

    public GameObject Bearer;
    public bool IsGoingHome;

    public void Start()
    {
        _original_location = transform.position;
        _original_rotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        if(IsGoingHome)
        {
            Vector3 direction = _original_location - gameObject.transform.position;

            _drift_timer += Time.deltaTime;

            if (_drift_timer >= _drift_seconds)
            {
                IsGoingHome = false;
                transform.SetPositionAndRotation(_original_location, _original_rotation);
            }
            else
            {
                float ratio = _drift_timer / _drift_seconds;

                transform.SetPositionAndRotation(
                    Vector3.Lerp(_drift_location, _original_location, ratio),
                    Quaternion.Slerp(_drift_rotation, _original_rotation, ratio)
                );
            }
        }
        else if(Bearer != null)
        {
            gameObject.transform.SetPositionAndRotation(Bearer.transform.localPosition, Bearer.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !IsGoingHome && Bearer == null)
        {
            // is the player who collided from... THE OPPOSITE TEAM!??!
            if (collision.gameObject.GetComponent<PlayerController>().Team != Team)
            {
                Bearer = collision.gameObject;
            }
        }
    }

    public void ReturnHome()
    {
        Bearer = null;
        IsGoingHome = true;
        _drift_location = transform.position;
        _drift_rotation = transform.rotation;
        _drift_seconds = (_original_location - _drift_location).magnitude / 10f;
        _drift_timer = 0;

        Rigidbody body = gameObject.GetComponent<Rigidbody>();
        body.velocity = Vector3.zero;
    }
}
