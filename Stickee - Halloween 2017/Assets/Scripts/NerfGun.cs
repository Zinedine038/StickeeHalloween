using System;
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

    private int curClip;
    public int maxClip = 8;
    bool empty = false;
    public AudioClip reloadSound;
    public ParticleSystem part;
    // Use this for initialization
    void Start () 
	{
        curClip=maxClip;
		source = GetComponent<AudioSource> ();
		vrtkInteract.InteractableObjectUsed += Fire;
	}
	
    public void PickUp()
    {
        source.PlayOneShot(reloadSound);
    }

    public void Fire(object o, InteractableObjectEventArgs e)
	{
        if(!empty)
        {
            part.Play();
            anim.SetTrigger("Fire");
            source.PlayOneShot(gunShot);
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            curClip--;
            if(curClip==0)
            {
                empty=true;
                StartCoroutine(Reload());
            }
        }

	}

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(0.5f);
        source.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(1.5f);
        empty=false;
        curClip=maxClip;
    }
}
