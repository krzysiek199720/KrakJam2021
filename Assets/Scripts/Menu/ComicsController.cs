using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicsController : MonoBehaviour
{
    public TextMeshProUGUI score_text;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Score") && score_text != null)
            score_text.text = "Score: " + PlayerPrefs.GetInt("Score", 0).ToString();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
