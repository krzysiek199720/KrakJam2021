using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicsController : MonoBehaviour
{
    public TextMeshProUGUI score_text;

    bool can = false;

    private void Start()
    {
        Invoke("DoCan", 9f);

        if (PlayerPrefs.HasKey("Score") && score_text != null)
            score_text.text = "Score: " + PlayerPrefs.GetFloat("Score", 0).ToString();
    }

    private void Update()
    {
        if (can && Input.anyKeyDown)
            LoadMenuScene();
    }

    void DoCan()
    {
        can = true;
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
