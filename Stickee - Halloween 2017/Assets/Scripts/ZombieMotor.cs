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
    public AudioSource source;
    // Use this for initialization
    void Start () {
        player = GameManager.instance.player;
		nav.SetDestination(player.transform.position);
        nav.speed=speed;    
        animator.SetFloat("Speed",speed);
        print(health);
        source = gameObject.AddComponent<AudioSource>();
        StartCoroutine(RandomMoans());

	}

    private IEnumerator RandomMoans()
    {
        while(true)
        {
            source.PlayOneShot(ZombieSounds.instance.moans[(UnityEngine.Random.Range(0,ZombieSounds.instance.moans.Length))]);
            yield return new WaitForSeconds(UnityEngine.Random.Range(7,12));
        }
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
        source.PlayOneShot(ZombieSounds.instance.attacks[(UnityEngine.Random.Range(0, ZombieSounds.instance.attacks.Length))]);
        yield return new WaitForSeconds(attackSpeed);
        mayAttack=true;
    }

    public void TakeDamage(int dmg)
    {
        health-=dmg;
        print(health);
        if(health<=0 && GetComponent<RagdollZombie>().isRegularMesh==false)
        {
            source.PlayOneShot(ZombieSounds.instance.deaths[(UnityEngine.Random.Range(0, ZombieSounds.instance.deaths.Length))]);
            StopAllCoroutines();
            FindObjectOfType<CurrentPlayerData>().zombiesKilled++;
            if (FindObjectOfType<CurrentPlayerData>().zombiesKilled==10)
            {
                GameManager.instance.StartCoroutine(GameManager.instance.FridgeEvent());
            }
            GetComponent<RagdollZombie>().ChangeToRegularMesh();
        }
    }

    private IEnumerator GetDamage()
    {
        print("lel" + Time.time);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<PlayerStats>().GetHit(damage);
    }
}
