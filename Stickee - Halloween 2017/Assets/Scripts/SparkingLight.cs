using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SparkingLight : MonoBehaviour {
    public AudioClip spark;
    public ParticleSystem part;
	// Use this for initialization
	void Awake () {
		StartCoroutine("Spark");
	}
	
	IEnumerator Spark()
    {
        while(true)
        {
            float delay = Random.Range(3,10);
            yield return new WaitForSeconds(delay);
            GetComponent<AudioSource>().PlayOneShot(spark);
            part.Play();
        }
    }
}
