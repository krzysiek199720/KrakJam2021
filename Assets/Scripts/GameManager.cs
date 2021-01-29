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

    public float ScoreMultiplier = 1f;

    private int coinsCollected = 0;

    private float timeToNextScore = 0f;
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

    private void FixedUpdate()
    {
        UpdateScoreMultiplier();
        timeToNextScore += Time.fixedDeltaTime;
        if(timeToNextScore >= 1f)
        {
            Score += gameManagerData.pointsPerSecond;
            timeToNextScore--;
        }
    }

    public void AddScore(int coins)
    {
        Score += coins * gameManagerData.pointsPerCoin * ScoreMultiplier;
        coinsCollected += coins;
        if(coinsCollected >= gameManagerData.coinsToUltimate)
        {
            Debug.Log("coin ult");
            coins -= gameManagerData.coinsToUltimate;
            playerController.AddPowerup(PowerupType.ULTIMATE);
        }
    }

    public void AddPowerupScore()
    {
        UpdateScoreMultiplier();
        Score += gameManagerData.pointsPerPowerup * ScoreMultiplier;
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
        Lifelines -= value;
        if(Lifelines < 1)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public float CalculateSpeedMultiplier()
    {
        int multiplierTimes = Mathf.FloorToInt(Score / gameManagerData.scoreSpeedupInterval);
        return 1f + multiplierTimes * gameManagerData.speedupMultiplierPerInterval;
    }
}
