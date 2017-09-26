using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FlashLight : MonoBehaviour {
    public VRTK_InteractableObject vrtk;
    public Light flashLight;
	// Use this for initialization
	void Start () {
		vrtk.InteractableObjectGrabbed+=ShineLight;
        vrtk.InteractableObjectUngrabbed+=StopLight;
	}

    private void StopLight(object sender, InteractableObjectEventArgs e)
    {
        flashLight.enabled = false;
    }

    private void ShineLight(object sender, InteractableObjectEventArgs e)
    {
        flashLight.enabled=true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
