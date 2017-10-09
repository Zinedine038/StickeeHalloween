using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Difficulty
{
    Wuss,
    BringItOn,
    BloodAndBrokenBones,
    DeathMarch
}

public class SpawnManager : MonoBehaviour
{
    private Difficulty difficulty;
    public GameObject childFriendlyZombie;
    public GameObject normalZombie;
    public GameObject roofZombie;
    public GameObject crawlZombie;
    public bool secondSpawnUnlocked = false;

    public bool roofSpawnUnlocked = false;
    public bool crawlerSpawnUnlocked = false;

    [Header("SpawnPoints")]
    public Transform spawnPointOne;
    public Transform spawnPointTwo;
    public Transform spawnPointThree;
    public Transform spawnPointGround;
    public Transform spawnPointRoof;

    [Header("Zombie Stats")]
    [SerializeField]
    public ZombieStats easy;
    public ZombieStats medium;
    public ZombieStats hard;
    public ZombieStats extreme;


    private int zombiesSpawned;

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        //TODO: FOR TESTING PURPOSES REMOVE BEFORE BUILD
        if(Input.GetKeyDown(KeyCode.K))
        {
            List<Transform> locs = new List<Transform>();
            locs.Add(spawnPointOne);
            locs.Add(spawnPointThree);
            if (secondSpawnUnlocked)
            {
                locs.Add(spawnPointTwo);
            }
            if (roofSpawnUnlocked)
            {
                locs.Add(spawnPointRoof);
            }
            Spawn(difficulty, ChooseSpawn(locs));
        }
    }

    private void InitializeGame()
    {
        difficulty = GameManager.instance.difficulty;
        switch (difficulty)
        {
            case Difficulty.Wuss:
                StartCoroutine(StartSpawningRythmEasy());
                break;
            case Difficulty.BringItOn:
                StartCoroutine(StartSpawningRythmMedium());
                break;
            case Difficulty.BloodAndBrokenBones:
                StartCoroutine(StartSpawningRythmHard());
                break;
            case Difficulty.DeathMarch:
                StartCoroutine(StartSpawningRythmExtrme());
                break;
        }
    }

    private IEnumerator StartSpawningRythmExtrme()
    {
        while (true)
        {
            List<Transform> locs = new List<Transform>();
            locs.Add(spawnPointOne);
            locs.Add(spawnPointThree);
            if (secondSpawnUnlocked)
            {
                locs.Add(spawnPointTwo);
            }
            if (roofSpawnUnlocked)
            {
                locs.Add(spawnPointRoof);
            }
            if(crawlerSpawnUnlocked)
            {
                locs.Add(spawnPointGround);
            }
            Spawn(difficulty, ChooseSpawn(locs));
            yield return new WaitForSeconds(DecideSpawnInterval(difficulty));
        }
    }
    private IEnumerator StartSpawningRythmHard()
    {
        while (true)
        {
            List<Transform> locs = new List<Transform>();
            locs.Add(spawnPointOne);
            locs.Add(spawnPointThree);
            if (secondSpawnUnlocked)
            {
                locs.Add(spawnPointTwo);
            }
            if(roofSpawnUnlocked)
            {
                locs.Add(spawnPointRoof);
            }
            Spawn(difficulty, ChooseSpawn(locs));
            yield return new WaitForSeconds(DecideSpawnInterval(difficulty));
        }
    }
    private IEnumerator StartSpawningRythmMedium()
    {
        while(true)
        {
            List<Transform> locs = new List<Transform>();
            locs.Add(spawnPointOne);
            locs.Add(spawnPointThree);
            if(secondSpawnUnlocked)
            {
                locs.Add(spawnPointTwo);
            }
            Spawn(difficulty,ChooseSpawn(locs));
            yield return new WaitForSeconds(DecideSpawnInterval(difficulty));
        }
    }
    private IEnumerator StartSpawningRythmEasy()
    {
        throw new NotImplementedException();
    }

    private Transform ChooseSpawn(List<Transform> possibleLocations)
    {
        int rand = UnityEngine.Random.Range(0,possibleLocations.Count);
        return possibleLocations[rand];
    }

    public void Spawn(Difficulty difficulty, Transform location)
    {
        switch (difficulty)
        {
            case Difficulty.Wuss:
                SpawnEasyZombie(easy,location);
                break;
            case Difficulty.BringItOn:
                SpawnAvarageZombie(medium, location);
                break;
            case Difficulty.BloodAndBrokenBones:
                SpawnHardZombie(hard, location);
                break;
            case Difficulty.DeathMarch:
                SpawnExtremeZombie(extreme, location);
                break;
        }
        zombiesSpawned++;
        print(zombiesSpawned);
        if(zombiesSpawned>=15 && !roofSpawnUnlocked)
        {
            if(difficulty == Difficulty.BloodAndBrokenBones || difficulty == Difficulty.DeathMarch)
            {
                StartCoroutine(GameManager.instance.UnlockRoofZombie());
            }
        }

        if (zombiesSpawned >= 25 && !crawlerSpawnUnlocked && difficulty == Difficulty.DeathMarch)
        {

            StartCoroutine(GameManager.instance.UnlockGroundZombies());
        }
    }

    private void SpawnExtremeZombie(ZombieStats stats, Transform location)
    {
        ZombieMotor zm = normalZombie.GetComponent<ZombieMotor>();
        zm.health = stats.health;
        zm.speed = DecideMotorSpeed(stats.startingSpeed, stats.maxSpeed);
        zm.damage = stats.damage;
        zm.attackSpeed = stats.attackInterval;
        if (location == spawnPointRoof)
        {
            Instantiate(roofZombie, location.transform.position, roofZombie.transform.rotation);

        }
        else if(location == spawnPointGround)
        {
            Instantiate(crawlZombie, location.transform.position, crawlZombie.transform.rotation);
        }
        else
        {
            Instantiate(normalZombie, location.transform.position, normalZombie.transform.rotation);
        }
    }
    private void SpawnHardZombie(ZombieStats stats, Transform location)
    {
        ZombieMotor zm = normalZombie.GetComponent<ZombieMotor>();
        zm.health = stats.health;
        zm.speed = DecideMotorSpeed(stats.startingSpeed, stats.maxSpeed);
        zm.damage = stats.damage;
        zm.attackSpeed = stats.attackInterval;
        if(location==spawnPointRoof)
        {
            Instantiate(roofZombie, location.transform.position, roofZombie.transform.rotation);

        }
        else
        {
            Instantiate(normalZombie, location.transform.position, normalZombie.transform.rotation);
        }
    }
    private void SpawnAvarageZombie(ZombieStats stats, Transform location)
    {
        ZombieMotor zm = normalZombie.GetComponent<ZombieMotor>();
        zm.health = stats.health;
        zm.speed = DecideMotorSpeed(stats.startingSpeed,stats.maxSpeed);
        zm.damage = stats.damage;
        zm.attackSpeed = stats.attackInterval;
        Instantiate(normalZombie,location.transform.position,normalZombie.transform.rotation);
    }
    private void SpawnEasyZombie(ZombieStats stats, Transform location)
    {

    }


    private float DecideMotorSpeed(float min, float max)
    {
        if(zombiesSpawned<5)
        {
            return min;
        }
        else if(zombiesSpawned >= 5 && zombiesSpawned < 15)
        {
            return UnityEngine.Random.Range(min,max*0.75f);
        }
        else if(zombiesSpawned>=15 && zombiesSpawned < 25)
        {
            return UnityEngine.Random.Range(min,max);
        }
        else if(zombiesSpawned>=25)
        {
            return max;
        }
        return 0.3f;
    }

    private float DecideSpawnInterval(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Wuss:
                return GenerateNumberBySpawnedNumber(5,10);
            case Difficulty.BringItOn:
                return GenerateNumberBySpawnedNumber(3,10);
            case Difficulty.BloodAndBrokenBones:
                return GenerateNumberBySpawnedNumber(3, 7);
            case Difficulty.DeathMarch:
                return GenerateNumberBySpawnedNumber(1,5);
        }
        return 0;
    }

    private float GenerateNumberBySpawnedNumber(float min, float max)
    {
        if (zombiesSpawned < 5)
        {
            return max;
        }
        else if (zombiesSpawned >= 5 && zombiesSpawned < 15)
        {
            return UnityEngine.Random.Range(min, max*0.75f);
        }
        else if (zombiesSpawned >= 15 && zombiesSpawned < 25)
        {
            return UnityEngine.Random.Range(min, max*0.5f);
        }
        else if (zombiesSpawned >= 25)
        {
            return min;
        }
        return min;
    }

    [System.Serializable]
public class ZombieStats
{
    public int health = 100;
    public float startingSpeed;
    public float maxSpeed;
    public int damage;
    public float attackInterval;
}
}