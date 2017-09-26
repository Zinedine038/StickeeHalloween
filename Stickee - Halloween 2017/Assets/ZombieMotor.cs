using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieMotor : MonoBehaviour {
    public NavMeshAgent nav;
    public GameObject player;
	// Use this for initialization
	void Start () {
		nav.SetDestination(player.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
