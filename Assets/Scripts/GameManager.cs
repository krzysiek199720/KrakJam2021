﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameManagerData gameManagerData;
    public static GameManager Instance { get; private set; }

    public PlayerController playerController;
    public GameObject fadeOutGo;

    public GameObject tutorial;

    public float Score { get; private set; }
    private int lifelines = 1;
    public int Lifelines { get { return lifelines; } private set { lifelines = value; if (lifelines < 1) { FadeOut(); Invoke("EndGame", 0.8f); } } }
    public bool isAlive { get { return Lifelines >= 1; } }

    public float ScoreMultiplier = 1f;

    private int coinsCollected = 0;

    private float timeToNextScore = 0f;
    private bool calculateScore = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Lifelines = gameManagerData.lifelines;

        AudioController.Instance.Play(SoundId.Gameplay_intro);
        float temp = AudioController.Instance.GetSoundLength(SoundId.Gameplay_intro);
        AudioController.Instance.Play(SoundId.Gameplay_loop, temp);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            StartGame();
            tutorial.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (calculateScore)
        {
            UpdateScoreMultiplier();
            timeToNextScore += Time.fixedDeltaTime;
            if(timeToNextScore >= 1f)
            {
                Score += gameManagerData.pointsPerSecond * ScoreMultiplier;
                timeToNextScore--;
            }
        }
    }

    public void StartGame()
    {
        playerController.shouldGameRun = true;
        calculateScore = true;
    }

    public void EndGame()
    {
        Debug.Log("Ending");
        playerController.shouldGameRun = false;
        
        calculateScore = false;

        PlayerPrefs.SetFloat("Score", Score);
        SceneManager.LoadScene(2);
    }

    public void FadeOut()
    {
        fadeOutGo.SetActive(true);
    }

    public void AddScore(int coins)
    {
        if (calculateScore)
        {
            AudioController.Instance.Play(SoundId.Collect_Banan);
            Score += coins * gameManagerData.pointsPerCoin * ScoreMultiplier;
            coinsCollected += coins;
            if(coinsCollected >= gameManagerData.coinsToUltimate)
            {
                coinsCollected -= gameManagerData.coinsToUltimate;
                playerController.AddPowerup(PowerupType.ULTIMATE);
            }
        }
    }

    public void AddPowerupScore()
    {
        if (calculateScore)
        {
            UpdateScoreMultiplier();
            Score += gameManagerData.pointsPerPowerup * ScoreMultiplier;
        }
    }

    private void UpdateScoreMultiplier()
    {
        if (playerController)
        {
            int powerupCount = playerController.powerups.Count;
            ScoreMultiplier = 1f + (powerupCount * gameManagerData.powerupMultiplier);
        }
    }

    
    public void TakeLifeline(int value)
    {
        // Shield is not taken if youre using ult or ghost
        if (playerController.powerups.ContainsKey(PowerupType.ULTIMATE)
            || playerController.powerups.ContainsKey(PowerupType.GHOST))
            return;

        AudioController.Instance.Play(SoundId.Monkey_hit);

        Lifelines -= value;
        if (!isAlive)
        {
            calculateScore = false;
            AudioController.Instance.Play(SoundId.Monkey_drop);
        }
        else if (Lifelines < 2)
        {
            playerController.shield.SetActive(false);
            AudioController.Instance.Play(SoundId.Collect_shield_deactivate);
        }
    }

    public void AddLifeline(int value)
    {
        Lifelines += value;
    }

    public float CalculateSpeedMultiplier()
    {
        int multiplierTimes = Mathf.FloorToInt(Score / gameManagerData.scoreSpeedupInterval);
        return 1f + multiplierTimes * gameManagerData.speedupMultiplierPerInterval;
    }

    public float UltimateProgress()
    {
        return (float)coinsCollected / gameManagerData.coinsToUltimate;
    }
}
