﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingCamera : MonoBehaviour {

	void FixedUpdate() {
        transform.Rotate(new Vector3(0, 0.1f, 0));
    }
}
