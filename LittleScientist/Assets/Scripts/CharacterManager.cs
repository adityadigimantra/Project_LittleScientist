using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [Header("Character Fields")]
    public GameObject CharacterPanel;
    public Image CharacterImage;
    public Image CharacterBgImage;
    public Image MessageBoxImage;
    public Text MessageBoxText;
    public Text NewElementText;
    public int fadeDuration=4;
    public string loadedString;
    public bool OpenPanelOnce = false;
    public bool shownOnce = false;

    [Header("MessageBox Data")]
    public Animator messageBoxAnimator;

    [Header("Instances")]
    public combinationManager combManager;
    public TutorialManager tutorialManager;

    public bool isCoroutineRunnning = false;
    private void Start()
    {
        combManager = FindObjectOfType<combinationManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
        messageBoxAnimator.SetBool("IsOpen", true);
    }

    // Update is called once per frame
    void Update()
    {
        loadedString = combManager.loadedString;
    }

    public void HandlingCharacterBehaviour(string message,int fontsize)
    {
        switch (combManager.currentElementState)
        {
            case combinationManager.ElementState.InitialState:
                ShowMessage(message,fontsize);
                Debug.Log("State is-Initial");
                break;

            case combinationManager.ElementState.NewElementFound:
                //string uppercaseWord = ConvertToUpperCase(message);
                ShowMessage(message,fontsize);
                Debug.Log("State is-New Element Found");
                break;
            case combinationManager.ElementState.ElementExists:
                ShowMessage(message,fontsize);
                //StartCoroutine(givingDelay());
                Debug.Log("State is-Element Exists");
                break;
            case combinationManager.ElementState.NoCombinationFound:
                ShowMessage(message,fontsize);
                //StartCoroutine(givingDelay());
                Debug.Log("State is-No Combination Found");
                break;
            case combinationManager.ElementState.IdleState:
                ShowMessage(message, fontsize);
                Debug.Log("State is-Idle");
                break;
        }

    }
    public void ShowMessage(string message,int fontSize)
    {
        CharacterPanel.SetActive(true);
        messageBoxAnimator.SetBool("IsOpen", true);
        MessageBoxImage.gameObject.SetActive(true);
        MessageBoxText.text = message;
        MessageBoxText.fontSize = fontSize;
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
        isCoroutineRunnning = true;
        CharacterPanel.SetActive(true);
        messageBoxAnimator.SetBool("IsOpen", true);
        MessageBoxImage.gameObject.SetActive(true);
        MessageBoxText.text = message;
        MessageBoxText.fontSize = fontSize;
        yield return new WaitForSeconds(time);
        messageBoxAnimator.SetBool("IsOpen", false);
        yield return new WaitForSeconds(1.5f);
        isCoroutineRunnning = false;
    }


    public void CloseCurrentMessage()
    {
        messageBoxAnimator.SetBool("IsOpen", false);
    }
}
