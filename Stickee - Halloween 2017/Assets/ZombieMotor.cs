using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieMotor : MonoBehaviour {
    public Animator animator;
    public NavMeshAgent nav;
    public GameObject player;
    public float speed;
	// Use this for initialization
	void Start () {
		nav.SetDestination(player.transform.position);
        nav.speed=speed;    
        animator.SetFloat("Speed",speed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator IncreaseMotorSpeed(float newSpeed)
    {
        while(animator.GetFloat("Speed")!=newSpeed)
        {
            animator.SetFloat("Speed",Mathf.Lerp(animator.GetFloat("Speed"),newSpeed,1*Time.deltaTime));
            yield return null;
        }
    }
}
