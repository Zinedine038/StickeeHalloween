using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int damage = 50;
	public float speed;
	public GameObject woodParticle;
	public AudioClip woodSFX;
    public AudioClip bloodSFX;
    public GameObject bloodParticle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * speed;
    }

    void OnCollisionEnter(Collision other)
	{
		GameObject impactParticle;
        Destroy(gameObject);
        if (other.gameObject.transform.tag=="BodyPart" && !other.gameObject.GetComponent<BodyPart>().isDead)
        {
            impactParticle = Instantiate(bloodParticle, transform.position, other.transform.rotation);
            AudioSource source = impactParticle.AddComponent<AudioSource>();
            source.PlayOneShot(bloodSFX);
            other.transform.parent.GetComponent<ZombieMotor>().TakeDamage(damage);
        }
        else
        {
            impactParticle = Instantiate(woodParticle, transform.position, other.transform.rotation);
            //AudioSource source = impactParticle.AddComponent<AudioSource>();
            //source.PlayOneShot(woodSFX);
        }

		Destroy (impactParticle, 2);
	}
}
