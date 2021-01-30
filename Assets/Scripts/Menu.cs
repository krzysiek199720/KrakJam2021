using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public AudioController audioController;

    // Start is called before the first frame update
    void Start()
    {
        audioController.Play(SoundId.Menu_intro);
        Invoke("PlayMainMusic", 5f);
    }

    public void PlayMainMusic()
    {
        audioController.Play(SoundId.Menu_loop);
    }
}
