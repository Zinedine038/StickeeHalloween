using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerStats : MonoBehaviour
{
    public float playerHealth;
    public AudioClip hitSound;
    public int score;
    // Use this for initialization
    void Start()
    {
        score=0;
    }    

    public void GetHit(float damage)
    {
        playerHealth -= damage;
        GetComponent<AudioSource>().PlayOneShot(hitSound);
    }

    public void Die()
    {
        //Like.. die or something
    }
}
