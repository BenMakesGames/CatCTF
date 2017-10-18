using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Flag : MonoBehaviour
{
    public Team Team;

    private Vector3 _original_location;

    private GameObject _bearer;
    private bool _returning_to_original_position;

    public void Start()
    {
        _original_location = GetComponent<Transform>().position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !_returning_to_original_position && _bearer == null)
        {
            // is the player who collided from... THE OPPOSITE TEAM!??!
            if (collision.gameObject.GetComponent<PlayerController>().Team != Team)
            {
                _bearer = collision.gameObject;
            }
            else
            {
                if (_original_location != gameObject.transform.position)
                    _returning_to_original_position = true;
            }
        }
    }
}
