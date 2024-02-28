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



    [Header("Def Settings Menu")]
    public GameObject ParentSettingsPanel;
    public GameObject def_settingsMenu;
    public GameObject[] def_musicButtons;
    public GameObject[] def_soundButtons;
    public GameObject resetProgressButton;
    public GameObject quitButton;

    [Header("for Settings Menu")]
    public GameObject for_settingsMenu;
    public GameObject[] for_musicButtons;
    public GameObject[] for_soundButtons;
    public GameObject for_resetProgressButton;
    public GameObject for_quitButton;

    [Header("for Settings Menu")]
    public GameObject aqua_settingsMenu;
    public GameObject[] aqua_musicButtons;
    public GameObject[] aqua_soundButtons;
    public GameObject aqua_resetProgressButton;
    public GameObject aqua_quitButton;


    public string selectedTheme;

    public string directoryPath;
    // Start is called before the first frame update

    private void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        elementManager = FindObjectOfType<ElementManager>();
        soundManager = FindObjectOfType<SoundManager>();
        directoryPath = Application.persistentDataPath;
        selectedTheme = PlayerPrefs.GetString("Theme");
        ConfigureSoundAndMusicOnSceneLoad();
    }
    private void Update()
    {
       
    }
    public void OpenSettingsMenu()
    {
        ParentSettingsPanel.SetActive(true);
        switch (selectedTheme)
        {
            case "Default":
                def_settingsMenu.SetActive(true);
                break;

            case "Forest":
                for_settingsMenu.SetActive(true);
                break;

            case "Aqua":
                aqua_settingsMenu.SetActive(true);
                break;

        }
       
        //Need to play animations.
    }
    public void CloseSettingsMenu()
    {
        ParentSettingsPanel.SetActive(false);
        switch (selectedTheme)
        {
            case "Default":
                def_settingsMenu.SetActive(false);
                break;

            case "Forest":
                for_settingsMenu.SetActive(false);
                break;

            case "Aqua":
                aqua_settingsMenu.SetActive(false);
                break;

        }
    }
    public void ResetProgressWhenClicked()
    {
        switch (selectedTheme)
        {
            case "Default":
                PlayerPrefs.DeleteAll();
                DeleteAllFiles();
                def_settingsMenu.SetActive(false);
                SceneManager.LoadScene(0);
                break;

            case "Forest":
                PlayerPrefs.DeleteAll();
                DeleteAllFiles();
                for_settingsMenu.SetActive(false);
                SceneManager.LoadScene(0);
                break;

            case "Aqua":
                PlayerPrefs.DeleteAll();
                DeleteAllFiles();
                aqua_settingsMenu.SetActive(false);
                SceneManager.LoadScene(0);
                break;

        }
       
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
        switch(selectedTheme)
        {
            case "Default":
                switch (music)
                {
                    case "off":
                        soundManager.StopBgMusic();
                        def_musicButtons[0].SetActive(false);
                        def_musicButtons[1].SetActive(true);
                        break;

                    case "on":
                        soundManager.PlayBgMusic();
                        def_musicButtons[0].SetActive(true);
                        def_musicButtons[1].SetActive(false);
                        break;
                }

                switch (sound)
                {
                    case "off":
                        soundManager.GameSoundsTurnedOFF();
                        def_soundButtons[1].SetActive(true);
                        def_soundButtons[0].SetActive(false);
                        break;
                    case "on":
                        soundManager.GameSoundsTurnedON();
                        def_soundButtons[1].SetActive(false);
                        def_soundButtons[0].SetActive(true);
                        break;
                }
                break;


            case "Forest":
                switch (music)
                {
                    case "off":
                        soundManager.StopBgMusic();
                        for_musicButtons[0].SetActive(false);
                        for_musicButtons[1].SetActive(true);
                        break;

                    case "on":
                        soundManager.PlayBgMusic();
                        for_musicButtons[0].SetActive(true);
                        for_musicButtons[1].SetActive(false);
                        break;
                }

                switch (sound)
                {
                    case "off":
                        soundManager.GameSoundsTurnedOFF();
                        for_soundButtons[1].SetActive(true);
                        for_soundButtons[0].SetActive(false);
                        break;
                    case "on":
                        soundManager.GameSoundsTurnedON();
                        for_soundButtons[1].SetActive(false);
                        for_soundButtons[0].SetActive(true);
                        break;
                }
                break;


            case "Aqua":
                switch (music)
                {
                    case "off":
                        soundManager.StopBgMusic();
                        aqua_musicButtons[0].SetActive(false);
                        aqua_musicButtons[1].SetActive(true);
                        break;

                    case "on":
                        soundManager.PlayBgMusic();
                        aqua_musicButtons[0].SetActive(true);
                        aqua_musicButtons[1].SetActive(false);
                        break;
                }

                switch (sound)
                {
                    case "off":
                        soundManager.GameSoundsTurnedOFF();
                        aqua_soundButtons[1].SetActive(true);
                        aqua_soundButtons[0].SetActive(false);
                        break;
                    case "on":
                        soundManager.GameSoundsTurnedON();
                        aqua_soundButtons[1].SetActive(false);
                        aqua_soundButtons[0].SetActive(true);
                        break;
                }
                break;

        }

    }
    public void TurnOffMusic()
    {
        switch (selectedTheme)
        {
            case "Default":
                soundManager.StopBgMusic();
                def_musicButtons[0].SetActive(false);
                def_musicButtons[1].SetActive(true);
                PlayerPrefs.SetString("Music", "off");
                break;

            case "Forest":
                soundManager.StopBgMusic();
                for_musicButtons[0].SetActive(false);
                for_musicButtons[1].SetActive(true);
                PlayerPrefs.SetString("Music", "off");
                break;

            case "Aqua":
                soundManager.StopBgMusic();
                aqua_musicButtons[0].SetActive(false);
                aqua_musicButtons[1].SetActive(true);
                PlayerPrefs.SetString("Music", "off");
                break;

        }

    }
    public void TurnOnMusic()
    {
        switch (selectedTheme)
        {
            case "Default":
                soundManager.PlayBgMusic();
                def_musicButtons[0].SetActive(true);
                def_musicButtons[1].SetActive(false);
                PlayerPrefs.SetString("Music", "on");
                break;

            case "Forest":
                soundManager.PlayBgMusic();
                for_musicButtons[0].SetActive(true);
                for_musicButtons[1].SetActive(false);
                PlayerPrefs.SetString("Music", "on");
                break;

            case "Aqua":
                soundManager.PlayBgMusic();
                aqua_musicButtons[0].SetActive(true);
                aqua_musicButtons[1].SetActive(false);
                PlayerPrefs.SetString("Music", "on");
                break;

        }
      
    }
    public void TurnOffSound()
    {
        switch (selectedTheme)
        {
            case "Default":
                soundManager.GameSoundsTurnedOFF();
                def_soundButtons[1].SetActive(true);
                def_soundButtons[0].SetActive(false);
                PlayerPrefs.SetString("Sound", "off");
                break;

            case "Forest":
                soundManager.GameSoundsTurnedOFF();
                for_soundButtons[1].SetActive(true);
                for_soundButtons[0].SetActive(false);
                PlayerPrefs.SetString("Sound", "off");
                break;

            case "Aqua":
                soundManager.GameSoundsTurnedOFF();
                aqua_soundButtons[1].SetActive(true);
                aqua_soundButtons[0].SetActive(false);
                PlayerPrefs.SetString("Sound", "off");
                break;

        }
       
    }
    public void TurnOnSound()
    {
        switch (selectedTheme)
        {
            case "Default":
                soundManager.GameSoundsTurnedON();
                def_soundButtons[1].SetActive(false);
                def_soundButtons[0].SetActive(true);
                PlayerPrefs.SetString("Sound", "on");
                break;

            case "Forest":
                soundManager.GameSoundsTurnedON();
                for_soundButtons[1].SetActive(false);
                for_soundButtons[0].SetActive(true);
                PlayerPrefs.SetString("Sound", "on");
                break;

            case "Aqua":
                soundManager.GameSoundsTurnedON();
                aqua_soundButtons[1].SetActive(false);
                aqua_soundButtons[0].SetActive(true);
                PlayerPrefs.SetString("Sound", "on");
                break;

        }
        
    }

    #endregion



    #region All Files Created Data
    public void DeleteAllFiles()
    {
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
