using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerStats : MonoBehaviour
{
    public float playerHealth;
    public AudioClip hitSound;
    public int score;
    AudioSource source;
    // Use this for initialization
    void Start()
    {
        score=0;
        source=GetComponent<AudioSource>();
    }    

    public void GetHit(float damage)
    {
        playerHealth -= damage;
        GetComponent<AudioSource>().PlayOneShot(hitSound);
        if(playerHealth<=0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.instance.GameOver();
        GameManager.instance.PlayerDied();
        //Save Score
    }
}
