using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [Header("Character Fields")]
    public GameObject CharacterPanel;
    public Image boyCharacterImage;
    public Image girlCharacterImage;
    public Image CharacterBgImage;
    public Text NewElementText;
    public int fadeDuration=4;
    public string loadedString;
    public bool OpenPanelOnce = false;
    public bool shownOnce = false;



    [Header("Theme Message Boxes")]
    public Image def_MessageBoxImage;
    public Text def_MessageBoxText;
    public Animator def_messageBoxAnimator;
    public Image for_MessageBoxImage;
    public Text for_MessageBoxText;
    public Animator for_messageBoxAnimator;
    public Image aqua_MessageBoxImage;
    public Text aqua_MessageBoxText;
    public Animator aqua_messageBoxAnimator;

    public string selectedTheme;

    [Header("Instances")]
    public combinationManager combManager;
    public TutorialManager tutorialManager;

    public bool isCoroutineRunnning = false;
    private void Start()
    {
        selectedTheme = PlayerPrefs.GetString("Theme");
        combManager = FindObjectOfType<combinationManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();

        switch (selectedTheme)
        {
            case "Default":
                def_messageBoxAnimator.SetBool("IsOpen", true);
                break;

            case "Forest":
                for_messageBoxAnimator.SetBool("IsOpen", true);
                break;

            case "Aqua":
                aqua_messageBoxAnimator.SetBool("IsOpen", true);
                break;
        }
        
       

        CheckCharacterChoosenByPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        loadedString = combManager.loadedString;
    }
    public void CheckCharacterChoosenByPlayer()
    {
        string boycharacter = "boy";
        string girlcharacter = "girl";
        if(PlayerPrefs.GetString("CharacterChoosen") ==boycharacter)
        {
            boyCharacterImage.gameObject.SetActive(true);
            girlCharacterImage.gameObject.SetActive(false);

        }
        if(PlayerPrefs.GetString("CharacterChoosen") ==girlcharacter)
        {
            girlCharacterImage.gameObject.SetActive(true);
            boyCharacterImage.gameObject.SetActive(false);
        }
        
    }
    public void HandlingCharacterBehaviour(string message,int fontsize)
    {
        switch (combManager.currentElementState)
        {
            case combinationManager.ElementState.InitialState:
                ShowMessage(message,fontsize);
               
                break;

            case combinationManager.ElementState.NewElementFound:
                //string uppercaseWord = ConvertToUpperCase(message);
                ShowMessage(message,fontsize);
               
                break;
            case combinationManager.ElementState.ElementExists:
                ShowMessage(message,fontsize);
                //StartCoroutine(givingDelay());
               
                break;
            case combinationManager.ElementState.NoCombinationFound:
                ShowMessage(message,fontsize);
                //StartCoroutine(givingDelay());
                
                break;
            case combinationManager.ElementState.IdleState:
                ShowMessage(message, fontsize);
               
                break;
        }

    }
    public void ShowMessage(string message,int fontSize)
    {
        switch(selectedTheme)
        {
            case "Default":
                CharacterPanel.SetActive(true);
                def_messageBoxAnimator.SetBool("IsOpen", true);
                def_MessageBoxImage.gameObject.SetActive(true);
                def_MessageBoxText.text = message;
                def_MessageBoxText.fontSize = fontSize;
                for_MessageBoxImage.gameObject.SetActive(false);
                aqua_MessageBoxImage.gameObject.SetActive(false);
                break;

            case "Forest":
                CharacterPanel.SetActive(true);
                for_messageBoxAnimator.SetBool("IsOpen", true);
                for_MessageBoxImage.gameObject.SetActive(true);
                for_MessageBoxText.text = message;
                for_MessageBoxText.fontSize = fontSize;

                def_MessageBoxImage.gameObject.SetActive(false);
                aqua_MessageBoxImage.gameObject.SetActive(false);
                break;

            case "Aqua":
                CharacterPanel.SetActive(true);
                aqua_messageBoxAnimator.SetBool("IsOpen", true);
                aqua_MessageBoxImage.gameObject.SetActive(true);
                aqua_MessageBoxText.text = message;
                aqua_MessageBoxText.fontSize = fontSize;

                for_MessageBoxImage.gameObject.SetActive(false);
                def_MessageBoxImage.gameObject.SetActive(false);
                break;
        }

    }


    public string ConvertToUpperCase(string input)
    {
        if(string.IsNullOrEmpty(input))
        {
            return input;

        }
        else
        {
            return char.ToUpper(input[0])+input.Substring(1);
        }
    }
    IEnumerator DisplayMessage(string message,int time,int fontSize)
    {
        switch(selectedTheme)
        {
            case "Default":
                isCoroutineRunnning = true;
                CharacterPanel.SetActive(true);
                def_messageBoxAnimator.SetBool("IsOpen", true);
                def_MessageBoxImage.gameObject.SetActive(true);
                def_MessageBoxText.text = message;
                def_MessageBoxText.fontSize = fontSize;
                yield return new WaitForSeconds(time);
                def_messageBoxAnimator.SetBool("IsOpen", false);
                yield return new WaitForSeconds(1.5f);
                isCoroutineRunnning = false;
                break;

            case "Forest":
                isCoroutineRunnning = true;
                CharacterPanel.SetActive(true);
                for_messageBoxAnimator.SetBool("IsOpen", true);
                for_MessageBoxImage.gameObject.SetActive(true);
                for_MessageBoxText.text = message;
                for_MessageBoxText.fontSize = fontSize;
                yield return new WaitForSeconds(time);
                for_messageBoxAnimator.SetBool("IsOpen", false);
                yield return new WaitForSeconds(1.5f);
                isCoroutineRunnning = false;
                break;

            case "Aqua":
                isCoroutineRunnning = true;
                CharacterPanel.SetActive(true);
                aqua_messageBoxAnimator.SetBool("IsOpen", true);
                aqua_MessageBoxImage.gameObject.SetActive(true);
                aqua_MessageBoxText.text = message;
                aqua_MessageBoxText.fontSize = fontSize;
                yield return new WaitForSeconds(time);
                aqua_messageBoxAnimator.SetBool("IsOpen", false);
                yield return new WaitForSeconds(1.5f);
                isCoroutineRunnning = false;
                break;
        }
        
    }


    public void CloseCurrentMessage()
    {
        switch (selectedTheme)
        {
            case "Default":
                def_messageBoxAnimator.SetBool("IsOpen", false);
                break;

            case "Forest":
                for_messageBoxAnimator.SetBool("IsOpen", false);
                break;

            case "Aqua":
                aqua_messageBoxAnimator.SetBool("IsOpen", false);
                break;
        }
        
       
    }
}
