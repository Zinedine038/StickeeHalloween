using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieMotor : MonoBehaviour {
    public Animator animator;
    public NavMeshAgent nav;
    public GameObject player;
    public float speed;
    private bool mayAttack = true;

    // Use this for initialization
    void Start () {
		nav.SetDestination(player.transform.position);
        nav.speed=speed;    
        animator.SetFloat("Speed",speed);
	}
	
	// Update is called once per frame
	void Update () {
        nav.updateRotation=true;
        if (Vector3.Distance(transform.position,player.transform.position)<=1)
        {
            nav.speed=0;
            animator.SetFloat("Speed",Mathf.Lerp(animator.GetFloat("Speed"),0,1*Time.deltaTime));
            Attack();
        }
        else
        {
            nav.speed = speed;
            nav.SetDestination(player.transform.position);
        }
    }

    IEnumerator IncreaseMotorSpeed(float newSpeed)
    {
        while(animator.GetFloat("Speed")!=newSpeed)
        {
            animator.SetFloat("Speed",Mathf.Lerp(animator.GetFloat("Speed"),newSpeed,1*Time.deltaTime));
            yield return null;
        }
    }

    void Attack()
    {
        if(mayAttack)
        {
            StartCoroutine("AttackPlayer");
        }
    }

    private IEnumerator AttackPlayer()
    {
        mayAttack=false;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        //hpdown
        mayAttack=true;
    }
}
