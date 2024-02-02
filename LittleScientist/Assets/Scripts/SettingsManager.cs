using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [Header("Instance")]
    public combinationManager comManager;
    public ElementManager elementManager;
    public ElementPosition elementPosition;
    public SoundManager soundManager;
    [Header("Buttons and More")]
    public GameObject settingsMenu;
    public GameObject[] musicButtons;
    public GameObject[] soundButtons;

    public GameObject themeButton;
    public GameObject resetProgressButton;
    public GameObject quitButton;

    [Header("Buttons Images")]
    public Image musicOffImage;
    public Image soundOffImage;

    public string directoryPath;
    // Start is called before the first frame update

    private void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        elementManager = FindObjectOfType<ElementManager>();
        soundManager = FindObjectOfType<SoundManager>();
        directoryPath = Application.persistentDataPath;
        ConfigureSoundAndMusicOnSceneLoad();
    }
    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        //Need to play animations.
    }
    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
        ////Need to play animations.
    }
    public void ResetProgressWhenClicked()
    {
        PlayerPrefs.DeleteAll();
        DeleteAllFiles();
        settingsMenu.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

    #region Sounds/Music On/Off

    public void ConfigureSoundAndMusicOnSceneLoad()
    {
        string music = PlayerPrefs.GetString("Music");
        string sound = PlayerPrefs.GetString("Sound");

        switch (music)
        {
            case "off":
                soundManager.StopBgMusic();
                musicButtons[0].SetActive(false);
                musicButtons[1].SetActive(true);
                break;

            case "on":
                soundManager.PlayBgMusic();
                musicButtons[0].SetActive(true);
                musicButtons[1].SetActive(false);
                break;
        }

        switch(sound)
        {
            case "off":
                soundManager.GameSoundsTurnedOFF();
                soundButtons[1].SetActive(true);
                soundButtons[0].SetActive(false);
                break;
            case "on":
                soundManager.GameSoundsTurnedON();
                soundButtons[1].SetActive(false);
                soundButtons[0].SetActive(true);
                break;
        }

    }
    public void TurnOffMusic()
    {
        soundManager.StopBgMusic();
        musicButtons[0].SetActive(false);
        musicButtons[1].SetActive(true);
        PlayerPrefs.SetString("Music", "off");
    }
    public void TurnOnMusic()
    {
        soundManager.PlayBgMusic();
        musicButtons[0].SetActive(true);
        musicButtons[1].SetActive(false);
        PlayerPrefs.SetString("Music", "on");
    }
    public void TurnOffSound()
    {
        soundManager.GameSoundsTurnedOFF();
        soundButtons[1].SetActive(true);
        soundButtons[0].SetActive(false);
        PlayerPrefs.SetString("Sound", "off");
    }
    public void TurnOnSound()
    {
        soundManager.GameSoundsTurnedON();
        soundButtons[1].SetActive(false);
        soundButtons[0].SetActive(true);
        PlayerPrefs.SetString("Sound", "on");
    }

    #endregion



    #region All Files Created Data
    public void DeleteAllFiles()
    {
        //DeleteCreatedElementsFile();
        //DeleteSaveElementPositionsFile();
        //DeleteDisabledGameObjectsFile();
        //DeleteSaveFinalElementPositionFile();
        if(Directory.Exists(directoryPath))
        {
            string[] files = Directory.GetFiles(directoryPath);
            foreach(string file in files)
            {
                File.Delete(file);
            }
            Debug.Log("All Files Deleted");
        }
        else
        {
            Debug.Log("Files Not Found!");
        }
    }
    #endregion
}
