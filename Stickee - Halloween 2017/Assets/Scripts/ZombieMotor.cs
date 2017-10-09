using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

public class ZombieMotor : MonoBehaviour {
    public Animator animator;
    public NavMeshAgent nav;
    public GameObject player;
    public float speed;
    public float stoppingDistance = 2;
    private bool mayAttack = true;
    public float attackSpeed = 2.633f;
    public int damage;
    public int health;
    // Use this for initialization
    void Start () {
        player = GameManager.instance.player;
		nav.SetDestination(player.transform.position);
        nav.speed=speed;    
        animator.SetFloat("Speed",speed);
        print(health);

	}
	
	// Update is called once per frame
	void Update () {
        
        nav.updateRotation=true;
        if (Vector3.Distance(transform.position,player.transform.position)<=stoppingDistance)
        {
            animator.SetFloat("Speed",Mathf.Lerp(animator.GetFloat("Speed"),0,1*Time.deltaTime));
            Attack();
            FaceTarget();
        }
        else
        {
            animator.SetFloat("Speed", speed);
        }
        nav.SetDestination(player.transform.position);

    }

    private void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*5);
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
        StartCoroutine(GetDamage());
        yield return new WaitForSeconds(attackSpeed);
        mayAttack=true;
    }

    public void TakeDamage(int dmg)
    {
        health-=dmg;
        print(health);
        if(health<=0)
        {

            GetComponent<RagdollZombie>().ChangeToRegularMesh();
        }
    }

    private IEnumerator GetDamage()
    {
        yield return new WaitForSeconds(1f);
        //PlayerGetDamagebruh
    }
}
