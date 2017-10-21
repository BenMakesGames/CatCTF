using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraEffects : MonoBehaviour {

    public float MAX_CAMERA_DISTANCE = 10;
    public float COLLISION_CUSHION = 3.41f;

    public float underwaterLevel = 0.35f;
    public Color underwaterColor = new Color(34 / 255f, 79 / 255f, 99 / 255f, 0.7f);
    public float underwaterDensity = 0.1f;

    public GameObject target;

    public Material sky;

    private Camera _camera;

    private bool underWater = false;

	void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -MAX_CAMERA_DISTANCE);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -GetCameraDistance());
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool wasUnderwater = underWater;

        underWater = transform.position.y < underwaterLevel;

        if(underWater && !wasUnderwater)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = underwaterColor;
            RenderSettings.fogDensity = underwaterDensity;

            RenderSettings.skybox.SetColor("_tint", underwaterColor);
            RenderSettings.skybox = null;
        }
        else if(!underWater && wasUnderwater)
        {
            RenderSettings.fog = false;
            RenderSettings.skybox = sky;
        }
    }
    
    public Vector3[] CalculateCameraClipPoints()
    {
        Vector3[] output = new Vector3[5];

        float z = _camera.nearClipPlane;
        float x = Mathf.Tan(_camera.fieldOfView / COLLISION_CUSHION) * z;
        float y = x / _camera.aspect;

        // top left
        output[0] = _camera.transform.position + (_camera.transform.rotation * new Vector3(-x, y, z));

        // top right
        output[1] = _camera.transform.position + (_camera.transform.rotation * new Vector3(x, y, z));

        // bottom left
        output[2] = _camera.transform.position + (_camera.transform.rotation * new Vector3(-x, -y, z));

        // bottom right
        output[3] = _camera.transform.position + (_camera.transform.rotation * new Vector3(x, -y, z));

        // center
        output[4] = _camera.transform.position + _camera.transform.forward;

        return output;
    }

    private float GetCameraDistance()
    {
        Vector3[] clipPoints = CalculateCameraClipPoints();

        float distance = MAX_CAMERA_DISTANCE;

        foreach(Vector3 clipPoint in clipPoints)
        {
            RaycastHit hit;

            if (Physics.Raycast(target.transform.position, clipPoint - target.transform.position, out hit))
            {
                if (hit.collider.gameObject.tag != "Does Not Block Camera" && hit.collider.gameObject.tag != "Player" && hit.distance < distance)
                    distance = hit.distance;
            }
        }

        return distance;
    }
}
