using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(AudioSource))]
public class NerfGun : MonoBehaviour {
	public Transform firePoint;
	public GameObject bullet;
	public VRTK_InteractableObject vrtkInteract;
	private AudioSource source;
	public AudioClip gunShot;
    public Animator anim;
	// Use this for initialization
	void Start () 
	{
		source = GetComponent<AudioSource> ();
		vrtkInteract.InteractableObjectUsed += Fire;
	}
	


	public void Fire(object o, InteractableObjectEventArgs e)
	{
        anim.SetTrigger("Fire");
		source.PlayOneShot (gunShot);
		Instantiate (bullet, firePoint.transform.position, firePoint.transform.rotation);
	}
}
