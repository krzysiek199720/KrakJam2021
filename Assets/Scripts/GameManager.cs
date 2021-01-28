using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameManagerData gameManagerData;
    public static GameManager Instance { get; private set; }

    public int Score { get; private set; }
    public int Lifelines { get; private set; }

    private float timeToNextScore = 0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        timeToNextScore += Time.fixedDeltaTime;
        if(timeToNextScore >= 1f)
        {
            Score += gameManagerData.pointsPerSecond;
            timeToNextScore--;
        }
    }

    public void AddScore(int coins)
    {
        Score += coins * gameManagerData.pointsPerCoin;
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
}
