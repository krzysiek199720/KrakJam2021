using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject comic;

    void Start()
    {
        AudioController.Instance.Play(SoundId.Menu_intro);
        AudioController.Instance.Play(SoundId.Menu_loop, 5f);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            comic.gameObject.SetActive(true);
        }
    }
}
