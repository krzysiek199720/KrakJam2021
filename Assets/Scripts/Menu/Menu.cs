using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    void Start()
    {
        AudioController.Instance.Play(SoundId.Menu_intro);
        Invoke("PlayMainMusic", 5f);
    }

    public void PlayMainMusic()
    {
        AudioController.Instance.Play(SoundId.Menu_loop);
    }
}
