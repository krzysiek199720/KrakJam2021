using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    public GameObject audioSources;
    public bool mute = false;
    public Sound[] soundsBase;


    private Dictionary<SoundId, AudioSource> sounds;

    private Dictionary<SoundId, AudioSource> activeSounds;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;

        sounds = new Dictionary<SoundId, AudioSource>();
        activeSounds = new Dictionary<SoundId, AudioSource>();
        populateSounds();
    }

    private void populateSounds()
    {
        foreach (Sound sound in soundsBase)
        {
            sounds.Add(sound.id, makeSource(sound));
        }
    }

    private AudioSource makeSource(Sound sound)
    {
        AudioSource source = audioSources.AddComponent<AudioSource>();
        source.clip = sound.clip;
        source.volume = sound.volume;
        source.pitch = sound.pitch;
        source.loop = sound.loop;

        return source;
    }

    public void Mute()
    {
        mute = true;
        foreach (var item in activeSounds)
        {
            item.Value.Stop();
        }
        activeSounds.Clear();
    }

    public void UnMute()
    {
        mute = false;
    }

    public void Play(SoundId id)
    {
        if (mute)
            return;

        if(sounds.ContainsKey(id))
        {
            AudioSource source = sounds[id];
            source.Play();

            activeSounds.Add(id, source);
        }
    }
    public void Play(SoundId id, float delay)
    {
        if (delay == 0f)
        {
            Play(id);
            return;
        }

        if (mute)
            return;

        if (sounds.ContainsKey(id))
        {
            AudioSource source = sounds[id];
            source.PlayDelayed(delay);

            activeSounds.Add(id, source);
        }
    }

    public void Stop(SoundId id)
    {
        if (activeSounds.ContainsKey(id))
        {
            activeSounds[id].Stop();
            activeSounds.Remove(id);
        }
    }

    public void Pause(SoundId id)
    {
        if (activeSounds.ContainsKey(id))
        {
            activeSounds[id].Pause();
        }
    }

    public void UnPause(SoundId id)
    {
        if (activeSounds.ContainsKey(id))
        {
            activeSounds[id].UnPause();
        }
    }

    public float GetSoundLength(SoundId id)
    {
        if (sounds.ContainsKey(id))
        {
            return sounds[id].clip.length;
        }

        //Return 0 if not found -- should not happen
        return 0f;
    }
}
