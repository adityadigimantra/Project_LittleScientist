using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    public AudioSource bgaudioSource;
    public AudioSource TapButtonAudioSource;
    public AudioSource ElementsCollideSound;
    public AudioSource newElementCreateSound;


    private void Awake()
    {
        if(_instance==null)
        {
            _instance = this;

        }
        else if(_instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        playBgMusic();
    }

    public void playBgMusic()
    {
        bgaudioSource.Play();
    }
    public void stopBgMusic()
    {
        bgaudioSource.Stop();
    }
    public void playButtonTapSound()
    {
        TapButtonAudioSource.Play();
    }
    public void elementCollideSound()
    {
        ElementsCollideSound.Play();
    }
    public void newElementCreatedSound()
    {
        newElementCreateSound.Play();
    }
}
