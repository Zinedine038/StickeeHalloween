using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    public static ZombieSounds instance;
	public AudioClip[] attacks;
    public AudioClip[] moans;
    public AudioClip[] deaths;
    public AudioClip[] zombieSlicedDeaths;
    public AudioClip zombieSliced;
    private void Start()
    {
        instance=this;
    }
}
