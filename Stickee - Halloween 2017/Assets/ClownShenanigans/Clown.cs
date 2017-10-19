using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Clown : MonoBehaviour {
    private AudioSource source;
    public AudioClip agony;
    public AudioClip scream, attack1, attack2,death,runningClown;
    bool phaseOneEnded = false;
    bool phaseTwoStarted = false;
    bool startedWalking = false;
    public int hp = 200;
    public Transform target;
    public GameObject ripperExplodeParticle;
    public Animator animator;
    public NavMeshAgent agent;
    private bool animatedWalk = false;
    private bool mayAttack = true;
    public GameObject gun;
    public GameObject gunparticle;
    public Transform gunSpot;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(ClownLogic());
        target =GameManager.instance.player.transform;
    }

    private void Update()
    {
        if(phaseOneEnded)
        {
            FaceTarget();
        }
        if(animatedWalk)
        {
            if(startedWalking)
            {
                agent.SetDestination(target.position);
            }
            animator.SetBool("Walk",true);          
        }
        if(Vector3.Distance(transform.position,target.position)<2)
        {
            StartCoroutine(Attack());
        }

    }

    IEnumerator ClownLogic()
    {       
        source.PlayOneShot(agony);
        yield return new WaitForSeconds(5f);
        StartCoroutine(PhaseTwo());
        phaseOneEnded=true;
    }

    private IEnumerator PhaseTwo()
    {
        animator.SetTrigger("Scream");
        source.PlayOneShot(scream);
        phaseTwoStarted = true;
        yield return new WaitForSeconds(1);
        animatedWalk = true;
        yield return new WaitForSeconds(2f);
        startedWalking = true;

        source.PlayOneShot(runningClown);
    }

    public void GetHit(int damage)
    {
        if (hp > 0)
        {
            if (!animatedWalk || !startedWalking)
            {
                source.Stop();
                StopAllCoroutines();
                Provoked();
            }
            hp -= damage;
            if (hp <= 0)
            {
                mayAttack = false;
                StopAllCoroutines();
                StartCoroutine(Death());
            }
        }

    }

    private void Provoked()
    {
        animator.SetTrigger("Provoked");
        animatedWalk = true;
        startedWalking = true;
        source.PlayOneShot(scream);
    }

    IEnumerator Attack()
    {
        if(mayAttack)
        {
            print("atk");
            int rand = UnityEngine.Random.Range(0, 2);
            mayAttack =false;
            if(rand==0)
            {
                animator.SetTrigger("Attack1");
                source.PlayOneShot(attack1);
                yield return new WaitForSeconds(1);
                FindObjectOfType<PlayerStats>().GetHit(30);
                StartCoroutine(WaitForAttack(0.5f));

            }
            else
            {
                animator.SetTrigger("Attack2");
                source.PlayOneShot(attack2);
                yield return new WaitForSeconds(1);
                FindObjectOfType<PlayerStats>().GetHit(30);
                StartCoroutine(WaitForAttack(1.5f));

            }
        }
    }

    IEnumerator WaitForAttack(float duration)
    {
        yield return new WaitForSeconds(duration);
        mayAttack=true;
    }


    IEnumerator Death()
    {
        gunSpot=GameObject.Find("AkimboGunSpot").transform;
        animator.SetTrigger("Death");
        source.PlayOneShot(death);
        startedWalking = false;
        Destroy(agent);
        yield return new WaitForSeconds(4f);
        Instantiate(gunparticle, gunSpot.transform.position, gunSpot.transform.rotation);
        GameManager.instance.instantiatedGun = Instantiate(gun, gunSpot.transform.position, gunSpot.transform.rotation);
        GameManager.instance.StartCoroutine(GameManager.instance.HandleSecondGun());
        GameManager.instance.spawnManager.StartCoroutine(GameManager.instance.spawnManager.ResumeSpawning());
        GameManager.instance.pointedLight.SetActive(false);
        GameManager.instance.spawnManager.lightning.StartCoroutine(GameManager.instance.spawnManager.lightning.LightningFlash(UnityEngine.Random.Range(1,3)));
        Destroy(gameObject);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        
    }
}
