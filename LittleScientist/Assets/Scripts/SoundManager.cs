using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    [Header("General Sounds")]
    public AudioSource bgaudioSource;
    public AudioSource elementsCollideSound;
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
    public AudioClip[] characterSoundUsedForTutorial;



    [Header("New Sounds(Tutorial)")]
    public AudioSource characterSoundForIntro;
    public AudioSource characterSoundForIntroducingElements;
    public AudioClip[] charactersSoundsforIntroMessages;
    public AudioClip[] charactersSoundsforIntroducingElements;
    public AudioClip tutorialCompleteSound;



    public AudioSource[] allSoundAudioSourcesInGame;
    public AudioSource[] allMusicAudioSourcesInGame;


    private void Start()
    {

    }
    public void PlayBgMusic()
    {
        bgaudioSource.Play();
    }
    public void StopBgMusic()
    {
        bgaudioSource.Stop();
    }

    public void GameSoundsTurnedON()
    {
        foreach (AudioSource audioSource in allSoundAudioSourcesInGame)
        {
            audioSource.mute = false;
        }
    }

    public void GameSoundsTurnedOFF()
    {
        foreach(AudioSource audioSource in allSoundAudioSourcesInGame)
        {
            audioSource.mute = true;
        }
    }
    public void elementCollideSound()
    {
        elementsCollideSound.Play();
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

    public void PlayCharacterSoundForIntro()
    {
        AudioClip clip = charactersSoundsforIntroMessages[Random.Range(0, charactersSoundsforIntroMessages.Length)];
        characterSoundForIntro.PlayOneShot(clip);
    }
    public void PlayCharacterSoundForIntroducingElements()
    {
        AudioClip clip = charactersSoundsforIntroducingElements[Random.Range(0, charactersSoundsforIntroducingElements.Length)];
        characterSoundForIntroducingElements.PlayOneShot(clip);
    }
}
