using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Ripper : VRTK_InteractableObject {
	public Animator anim;
	public AudioSource source;

	protected override void Awake ()
	{
		base.Awake ();
		anim.speed = 0;
	}

	public override void OnInteractableObjectGrabbed (InteractableObjectEventArgs e)
	{
		base.OnInteractableObjectGrabbed (e);
		StopAllCoroutines ();
		StartCoroutine (RampUpSpeed ());
	}
	public override void OnInteractableObjectUngrabbed (InteractableObjectEventArgs e)
	{
		base.OnInteractableObjectUngrabbed (e);
		StopAllCoroutines ();
		StartCoroutine (RampDownSpeed ());
	}

	IEnumerator RampUpSpeed()
	{
		while (anim.speed < 5) 
		{
			anim.speed +=1 * Time.deltaTime;
			anim.speed = Mathf.Clamp (anim.speed, 0, 5);
			source.pitch += 0.2f * Time.deltaTime;
			source.pitch = Mathf.Clamp (source.pitch, 0, 1);
			source.volume += 0.2f;
			source.volume = Mathf.Clamp (source.volume, 0, 1);
			yield return null;
		}
	}

	IEnumerator RampDownSpeed()
	{
		while (anim.speed > 0) 
		{
			anim.speed -= 1 * Time.deltaTime;
			anim.speed = Mathf.Clamp (anim.speed, 0, 5);
			source.pitch -= 0.2f * Time.deltaTime;
			source.pitch = Mathf.Clamp (source.pitch, 0, 1);
			source.volume -= 0.2f;
			source.volume = Mathf.Clamp (source.volume, 0, 1);
			yield return null;
		}
	}
}
