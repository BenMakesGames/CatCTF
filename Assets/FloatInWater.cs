using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatInWater : MonoBehaviour {

    public float waterLevel = 0.35f;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(transform.position.y < waterLevel)
        {
            body.AddForce(Vector3.up, ForceMode.Force);
        }
    }
}
