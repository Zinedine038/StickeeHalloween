﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWalk : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * speed;			
	}
}
