using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraEffects : MonoBehaviour {

    public float underwaterLevel = 0.35f;
    public Color underwaterColor = new Color(34 / 255f, 79 / 255f, 99 / 255f, 0.7f);
    public float underwaterDensity = 0.1f;

    public Material sky;

    private Camera _camera;

    private bool underWater = false;

	void Start()
    {
        _camera = GetComponent<Camera>();
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
}
