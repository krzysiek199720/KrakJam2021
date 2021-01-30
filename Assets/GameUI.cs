using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = "<size=40>Score</size>\n" + GameManager.Instance.Score.ToString();
        progressBar.value = GameManager.Instance.UltimateProgress();
    }
}
