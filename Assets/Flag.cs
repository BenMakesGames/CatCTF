using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Team Team;

    private Vector3 _original_location;

    public GameObject Bearer;
    public bool IsGoingHome;

    public void Start()
    {
        _original_location = GetComponent<Transform>().position;
    }

    private void FixedUpdate()
    {
        if(IsGoingHome)
        {

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
    }
}
