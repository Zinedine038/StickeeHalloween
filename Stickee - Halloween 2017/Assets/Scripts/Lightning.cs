using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource),typeof(Light))]
public class Lightning : MonoBehaviour 
{
	public AudioClip[] singleStrikes;
	public AudioClip doubleStrike;
	public AudioClip doubleStrikeSmall;
	private AudioSource source;
	private Light light;
	private void OnEnable()
	{
		source = GetComponent<AudioSource> ();
		light = GetComponent<Light> ();
	}

    public void Stop()
    {
        StopAllCoroutines();
    }

	public IEnumerator StrikeSingle()
	{
        LightningStrikeAudio(singleStrikes[Random.Range(0, singleStrikes.Length)]);
        float strikeIntensity = 25;
        light.intensity = strikeIntensity;
        yield return new WaitForSeconds(Random.Range(0.02f,0.05f));
        light.intensity=0;
    }

    private IEnumerator DoubleStrike(bool small)
	{
		if (!small) 
		{
			LightningStrikeAudio (doubleStrike);
			yield return new WaitForSeconds (3.8f);
		} 
		else 
		{
			LightningStrikeAudio (doubleStrikeSmall);
			yield return new WaitForSeconds (6.6f);
		}
	}

	private void LightningStrikeAudio(AudioClip sound)
	{
		source.PlayOneShot(sound);
	}

	public IEnumerator LightningFlash(float chaos)
	{
		yield return new WaitForSeconds (5);
		LightningStrikeAudio (singleStrikes [Random.Range (0, singleStrikes.Length)]);
		float strikeIntensity = Random.Range (12, 18);
		int struck = 0;
		while (struck < chaos) 
		{
			struck++;
			light.intensity = strikeIntensity;
			yield return new WaitForSeconds(Random.Range(0.02f,0.05f));
			light.intensity = 0;
			yield return new WaitForSeconds (Random.Range (0.02f, 0.05f));
		}
		StartCoroutine (LightningFlash (Random.Range(1,3)));
	}

}
