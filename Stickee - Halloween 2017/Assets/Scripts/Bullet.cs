using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float speed;
	public GameObject explosion;
	public AudioClip explosionSfx;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * speed;
	}

	void OnCollisionEnter(Collision other)
	{
		GameObject newExplosion = (GameObject) Instantiate (explosion, transform.position, other.transform.rotation);
		AudioSource source = newExplosion.AddComponent<AudioSource> ();
		source.PlayOneShot (explosionSfx);
		Explode ();
		Destroy (newExplosion, 2);
		Destroy (gameObject);
	}

	private void Explode()
	{
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, 3);
		foreach (Collider hit in colliders) {
            if(hit.transform.tag=="BodyPart")
            {
                hit.transform.parent.GetComponent<RagdollZombie>().ChangeToRegularMesh();
            }
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if (rb != null)
				rb.AddExplosionForce(300, explosionPos, 5, 3.0F);

		}

	}
}
