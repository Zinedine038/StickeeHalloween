using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    #region SingleTon
    public static GameManager instance;
    private void Awake()
    {
        instance=this;
    }
    #endregion

    public Difficulty difficulty;
    public ParticleSystem roofParticle;
    public GameObject roofLight;
    public AudioClip roofSound;
    public AudioSource source;
    public SpawnManager spawnManager;
    public GameObject player;

    public Rigidbody doorRB;
    public Collider doorCollider;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(UnlockGroundZombies());
        }
    }

    public IEnumerator UnlockRoofZombie()
    {
        source.PlayOneShot(roofSound);
        roofParticle.Play();
        yield return new WaitForSeconds(0.15f);
        roofLight.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(1);
        spawnManager.roofSpawnUnlocked=true;
        Destroy(roofLight,1f);
        spawnManager.roofSpawnUnlocked=true;
        spawnManager.Spawn(difficulty,spawnManager.spawnPointRoof);
    }

    public IEnumerator UnlockGroundZombies()
    {
        doorRB.useGravity=true;
        yield return new WaitForSeconds(2f);
        doorCollider.enabled=false;
        spawnManager.crawlerSpawnUnlocked=true;
    }

}
