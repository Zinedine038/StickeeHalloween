using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieMotor : MonoBehaviour {
    public Animator animator;
    public NavMeshAgent nav;
    public GameObject player;
    public float speed;
    public float stoppingDistance = 2;
    private bool mayAttack = true;
    public SkinnedMeshRenderer skin;
    // Use this for initialization
    void Start () {
		nav.SetDestination(player.transform.position);
        nav.speed=speed;    
        animator.SetFloat("Speed",speed);
        print(skin.sharedMesh.isReadable);
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
        yield return new WaitForSeconds(2.633f);
        //hpdown
        mayAttack=true;
    }
}
