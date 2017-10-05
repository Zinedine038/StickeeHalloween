using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,360*10*Time.deltaTime,0);
	}
}
