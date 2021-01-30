using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameManagerData gameManagerData;
    public static GameManager Instance { get; private set; }

    public PlayerController playerController;

    public float Score { get; private set; }
    public int Lifelines { get; private set; }
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
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartGame();
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

    public void AddScore(int coins)
    {
        if (calculateScore)
        {
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

        Lifelines -= value;
        if (!isAlive)
            calculateScore = false;
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
