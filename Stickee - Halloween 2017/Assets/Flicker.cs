using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour {
	public float flickerMax, flickerMin;
	public float maxSpeed, minSpeed;
	private float currentIntensity;
	private float lerpSpeed;
	public Light light;
	// Use this for initialization
	void Start () {
		StartCoroutine (FlickerLight());
	}
	
	// Update is called once per frame
	void Update () {
		light.intensity = Mathf.Lerp (light.intensity, currentIntensity, lerpSpeed * Time.deltaTime);
	}

	private IEnumerator FlickerLight()
	{
		while (true) 
		{
			currentIntensity=Random.Range(flickerMin,flickerMax);
			lerpSpeed = Random.Range (minSpeed, maxSpeed);
			yield return new WaitForSeconds(Random.Range(0.3f,0.5f));
		}
	}
}
