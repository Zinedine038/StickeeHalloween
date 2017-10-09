using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public bool isDead = false;
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "BodyPart")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

    }

}
