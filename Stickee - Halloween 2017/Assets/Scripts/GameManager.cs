using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region SingleTon
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Difficulty difficulty;
    public ParticleSystem roofParticle;
    public GameObject roofLight;
    public AudioClip roofSound;
    public AudioSource source;
    public AudioSource pitchedSource;
    public SpawnManager spawnManager;
    public GameObject player;
    public AudioClip gameOverSound;
    public GameObject roofPrefab;
    public GameObject clown;
    public Transform clownPosition;
    public AudioClip spookyClownMusic;
    public AudioClip creaking;
    private CurrentPlayerData currentData;
    private bool lightningFinished;
    public GameObject rotatingLight;
    public GameObject pointedLight;
    [Header("UI")]
    public CanvasGroup gameOverScreen;
    public CanvasGroup nameEnterScreen;
    public CanvasGroup menuCanvas;
    public TextMeshProUGUI scoreCounter;

    private void Start()
    {
        currentData = FindObjectOfType<CurrentPlayerData>();
    }

    private void Update()
    {

    }

    #region Initializations
    public void StartEasy()
    {
        difficulty = Difficulty.Wuss;
        StartCoroutine(FadeOutDifficulty());
    }
    public void StartMedium()
    {
        difficulty = Difficulty.BringItOn;
        StartCoroutine(FadeOutDifficulty());
    }
    public void StartHard()
    {
        difficulty = Difficulty.BloodAndBrokenBones;
        StartCoroutine(FadeOutDifficulty());
    }
    public void StartExtreme()
    {
        difficulty = Difficulty.DeathMarch;
        StartCoroutine(FadeOutDifficulty());
    }
    #endregion

    public void GameOver()
    {
        foreach (RagdollZombie rg in FindObjectsOfType<RagdollZombie>())
        {
            rg.ChangeToRegularMesh();
        }
        CalculateScore();
        source.PlayOneShot(gameOverSound);
        spawnManager.StopSpawning();
        gameOverScreen.gameObject.SetActive(true);
        gameOverScreen.DOFade(1, 3f);
        scoreCounter.text = currentData.score.ToString();
    }

    public void CalculateScore()
    {
        int calculatedScore=0;
        switch (difficulty)
        {
            case Difficulty.Wuss: 
                calculatedScore = (currentData.zombiesKilled*5);
                break;
            case Difficulty.BringItOn:
                calculatedScore = (currentData.zombiesKilled*10);
                break;
            case Difficulty.BloodAndBrokenBones:
                calculatedScore = (currentData.zombiesKilled*15);
                break;
            case Difficulty.DeathMarch:
                calculatedScore = (currentData.zombiesKilled*20);
                break;
        }
        currentData.score = calculatedScore;
    }

    public void NameConfirmed()
    {
        FindObjectOfType<CurrentPlayerData>().name= FindObjectOfType<CurrentPlayerData>().nameText.text;
        StartCoroutine(spawnManager.InitializeGame());
        StartCoroutine(FadeOutNameInput());
    }

    private IEnumerator FadeOutNameInput()
    {
        nameEnterScreen.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        nameEnterScreen.gameObject.SetActive(false);
    }

    public void ShowNameEnterScreen()
    {
        nameEnterScreen.gameObject.SetActive(true);
        nameEnterScreen.DOFade(1, 1f);
    }

    private IEnumerator FadeOutDifficulty()
    {
        menuCanvas.DOFade(0, 3f);
        foreach (Button button in menuCanvas.GetComponentsInChildren<Button>())
        {
            button.interactable = false;
        }
        yield return new WaitForSeconds(3f);
        menuCanvas.gameObject.SetActive(false);
        ShowNameEnterScreen();
    }


    #region Restart
    public void RebootScene()
    {
        source.Stop();
        FindObjectOfType<CurrentPlayerData>().PlayerDied();
        StartCoroutine(RestartScene());
    }

    private IEnumerator RestartScene()
    {
        spawnManager.ResetSpawner();
        roofLight = Instantiate(roofPrefab); 
        gameOverScreen.DOFade(0, 3f);
        yield return new WaitForSeconds(3f);
        gameOverScreen.gameObject.SetActive(false);
        spawnManager.menuCanvas.SetActive(true);
        foreach (Button button in spawnManager.menuCanvas.GetComponentsInChildren<Button>())
        {
            button.interactable = true;
        }
        spawnManager.lightning.Stop();
        spawnManager.menuCanvas.GetComponent<CanvasGroup>().DOFade(1, 1f);
    }
    #endregion

    #region unlocks
    public IEnumerator UnlockRoofZombie()
    {
        source.PlayOneShot(roofSound);
        roofParticle.Play();
        yield return new WaitForSeconds(0.15f);
        roofLight.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(1);
        spawnManager.roofSpawnUnlocked = true;
        Destroy(roofLight, 1f);
        spawnManager.roofSpawnUnlocked = true;
        spawnManager.Spawn(difficulty, spawnManager.spawnPointRoof);
    }

    public IEnumerator UnlockGroundZombies()
    {
        yield return new WaitForSeconds(2f);
        spawnManager.crawlerSpawnUnlocked = true;
    }

    public IEnumerator FridgeEvent()
    {
        //TODO Destroy Ripper!
        //Spawn Clown
        //Second gun!!!!
        foreach (RagdollZombie rg in FindObjectsOfType<RagdollZombie>())
        {
            rg.ChangeToRegularMesh();
        }
        spawnManager.PauseSpawning();
        spawnManager.lightning.Stop();
        StartCoroutine(ChaosLightning());
        lightningFinished=false;
        while(!lightningFinished)
        {
            yield return null;
        }
        pitchedSource.PlayOneShot(spookyClownMusic);
        StartCoroutine(FindObjectOfType<FridgeDoor>().CreakDoor());
        source.PlayOneShot(creaking);
        pointedLight.SetActive(true);
        yield return new WaitForSeconds(10);
        FindObjectOfType<FridgeDoor>().mayCreak=false;
        StartCoroutine(spawnManager.lightning.VeryIntenseSingleStrike());
        yield return new WaitForSeconds(0.02f);
        switch (difficulty)
        {
            case Difficulty.BloodAndBrokenBones:
                clown.GetComponent<Clown>().hp=300;
                break;
            case Difficulty.DeathMarch:
                clown.GetComponent<Clown>().hp=350;
                break;
        }
        Instantiate(clown,clownPosition.position,clown.transform.rotation);
    }

    private IEnumerator ChaosLightning()
    {
        for(int i = 0; i < 10; i++)
        {
            StartCoroutine(spawnManager.lightning.StrikeSingle());
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.3f,1f));
        }
        lightningFinished = true;
    }
    #endregion

}
