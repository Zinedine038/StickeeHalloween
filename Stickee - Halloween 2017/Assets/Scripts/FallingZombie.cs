using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FallingZombie : MonoBehaviour
{
    public ZombieMotor motor;
    public Animator anim;
    public NavMeshAgent nav;
    public Collider collider;
    public Rigidbody rb;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.tag == "Ground")
        {
            StartCoroutine("StartLanding");
        }
        else
        {
            Physics.IgnoreCollision(collision.collider,collider);
        }
    }

    private void FixedUpdate()
    {
        if(rb!=null)
        {
            rb.velocity = new Vector3(0, -10, 0);
        }
    }


    private IEnumerator StartLanding()
    {
        anim.SetTrigger("FinishedLanding");
        Destroy(rb);
        Destroy(collider);
        yield return null;
        nav.enabled=true;
        motor.enabled=true;  
    }
}
