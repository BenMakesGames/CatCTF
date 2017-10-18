using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public float walkSpeed = 10f;
    public float turnSpeed = 8f;
    public float lookSpeed = 2f;

    public float thrusterForce = 1000f;

    private PlayerMotor motor;
    public GameObject cameraArm;

    public float cameraArmAngle = 37.5f;

    public Team Team;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        cameraArmAngle = cameraArm.transform.eulerAngles.y;
	}

    void Update()
    {
        Move();
        Turn();
        Look();
    }

    private void Look()
    {
        cameraArmAngle -= Input.GetAxisRaw("Mouse Y") * lookSpeed;

        cameraArmAngle = Mathf.Clamp(cameraArmAngle, 0, 90);

        cameraArm.transform.localRotation = Quaternion.AngleAxis(cameraArmAngle, Vector3.right);
    }

    private void Turn()
    {
        Vector3 rotation = new Vector3(0, Input.GetAxisRaw("Mouse X"), 0) * turnSpeed;

        motor.Rotate(rotation);
    }

    private void Move()
    {
        // combine angle of camera and player
        float angle = transform.rotation.eulerAngles.y;

        // vertical (up/down arrow; forward/backwards) motion
        float verticalDx = Mathf.Sin(angle * Mathf.PI / 180);
        float verticalDz = Mathf.Cos(angle * Mathf.PI / 180);

        // horizontal (left/right arrow; strafing) motion
        float horizontalDx = Mathf.Sin((angle + 90) * Mathf.PI / 180);
        float horizontalDz = Mathf.Cos((angle + 90) * Mathf.PI / 180);

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 motion = new Vector3(
            v * verticalDx + h * horizontalDx,
            0,
            v * verticalDz + h * horizontalDz
        );

        if (motion.magnitude > 1) motion.Normalize();

        motion *= walkSpeed;

        motor.Move(motion);

        // jetpack

        Vector3 jetPackThrust = Vector3.zero;

        if(Input.GetButton("Jump"))
        {
            jetPackThrust = Vector3.up * thrusterForce;
        }

        motor.JetPack(jetPackThrust);
    }
}
