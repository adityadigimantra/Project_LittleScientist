using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    [Header("General Sounds")]
    public AudioSource bgaudioSource;
    public AudioSource ElementsCollideSound;
    public AudioSource newElementCreateSound;
    public AudioSource ArrowButtonSound;
    public AudioSource GeneralButtonSound;
    public AudioSource trashSound;

    [Header("Character Sounds")]
    public AudioSource backFromInactivitySound_Character;
    public AudioSource newElementFound_Character;
    public AudioSource combinationExists_Character;
    public AudioSource noCombinationFound_Character;
    public AudioSource welcomingSound_Character;
    public AudioClip[] newElementFound_Character_clips;


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
    public void elementCollideSound()
    {
        ElementsCollideSound.Play();
    }
    public void newElementCreatedSound()
    {
        newElementCreateSound.Play();
    }
    public void GetBackFromInactivitySound()
    {
        backFromInactivitySound_Character.Play();
    }
    public void playSound_Character_NewElementFound()
    {
        AudioClip clip = newElementFound_Character_clips[Random.Range(0, newElementFound_Character_clips.Length)];
        newElementFound_Character.PlayOneShot(clip);
    }

    public void CombinationAlreadyExistsSound()
    {
        combinationExists_Character.Play();
    }    
    public void NoCombinationFoundSound()
    {
        noCombinationFound_Character.Play();
    }
    public void PlayArrowButtonSound()
    {
        ArrowButtonSound.Play();
    }
    public void PlayGeneralButtonTapSound()
    {
        GeneralButtonSound.Play();
    }
    public void PlayCharacterWelcomingSound()
    {
        welcomingSound_Character.Play();
    }
    public void PlayCharacterInactivitySound()
    {
        welcomingSound_Character.Play();
    }
    public void PlayTrashSound()
    {
        trashSound.Play();
    }
}
