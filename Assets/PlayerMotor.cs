﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 look = Vector3.zero;
    private Vector3 jetPack = Vector3.zero;

    private Rigidbody rigidBody;
    private ConfigurableJoint joint;

    public float jointSpring = 20;
    public float jointMaxForce = 40;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        joint = GetComponent<ConfigurableJoint>();
    }

    public void Move(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void Rotate(Vector3 rotation)
    {
        this.rotation = rotation;
    }
    
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        if(rotation != Vector3.zero)
        {
            rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(rotation));
        }
    }
}
