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

    private Coroutine messageCoroutine;

    [Header("MessageBox Data")]
    public Animator messageBoxAnimator;

    [Header("Instances")]
    public combinationManager combManager;
    private void Start()
    {
        combManager = FindObjectOfType<combinationManager>();
        messageBoxAnimator.SetBool("IsOpen", true);
    }

    // Update is called once per frame
    void Update()
    {
        loadedString = combManager.loadedString;
    }

    public void HandlingCharacterBehaviour(string message,int time,int fontsize)
    {
        switch (combManager.currentElementState)
        {
            case combinationManager.ElementState.InitialState:
                ShowMessage(message,time, fontsize);
                Debug.Log("State is-Initial");
                break;

            case combinationManager.ElementState.NewElementFound:
                string uppercaseWord = ConvertToUpperCase(message);
                ShowMessage("Wow! New Element "+uppercaseWord+" is Found.",time, fontsize);
                Debug.Log("State is-New Element Found");
                break;
            case combinationManager.ElementState.ElementExists:
                ShowMessage(message,time, fontsize);
                //StartCoroutine(givingDelay());
                Debug.Log("State is-Element Exists");
                break;
            case combinationManager.ElementState.NoCombinationFound:
                ShowMessage(message,time, fontsize);
                //StartCoroutine(givingDelay());
                Debug.Log("State is-No Combination Found");
                break;
            case combinationManager.ElementState.IdleState:
                ShowMessage(message,time, fontsize);
                Debug.Log("State is-Idle");
                break;
        }

        //yield return fadeCharacterImage(0, 1);
        //yield return fadeCharacterImage(1,0);
    }
    public void HandlingCharacterBehaviourdefault(int time,int fontSize)
    {
        switch (combManager.currentElementState)
        {
            case combinationManager.ElementState.InitialState:
                ShowMessage("Let's Find New Elements",time,fontSize);
                Debug.Log("State is-Initial");
                break;
                //ToDo-Remove other cases
            case combinationManager.ElementState.NewElementFound:

                ShowMessage("Wow! New Element is Found.",time,fontSize);
                Debug.Log("State is-New Element Found");
                break;
            case combinationManager.ElementState.ElementExists:
                ShowMessage("Hmm! This Element already exists.",time,fontSize);
                Debug.Log("State is-Element Exists");
                break;
            case combinationManager.ElementState.NoCombinationFound:
                ShowMessage("Oops! I cannot find any combination for these",time,fontSize);
                Debug.Log("State is-No Combination Found");
                break;
        }
        //yield return fadeCharacterImage(0, 1);
        //yield return fadeCharacterImage(1,0);
    }
    public void ShowMessage(string message,int time,int fontSize)
    {
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }
        messageCoroutine = StartCoroutine(DisplayMessage(message,time, fontSize));
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
        CharacterPanel.SetActive(true);
        //messageBoxAnimator.SetBool("IsOpen", true);
        MessageBoxImage.gameObject.SetActive(true);
        MessageBoxText.text = message;
        MessageBoxText.fontSize = fontSize;
        yield return new WaitForSeconds(time);
        messageBoxAnimator.SetBool("IsOpen", false);
        //MessageBoxImage.gameObject.SetActive(false);
        //CharacterPanel.SetActive(false);
    }

    internal void HandlingCharacterBehaviour(Func<string> returnWelcomingMessages, int v1, int v2)
    {
        throw new NotImplementedException();
    }

    IEnumerator givingDelay()
    {
        yield return new WaitForSeconds(2);
        messageBoxAnimator.SetBool("IsOpen", false);
        //combManager.currentElementState = combinationManager.ElementState.InitialState;
        //HandlingCharacterBehaviourdefault();
    }

    IEnumerator switchOnOffCharacterPanel()
    {
        CharacterPanel.SetActive(true);
        yield return new WaitForSeconds(4f);
        CharacterPanel.SetActive(false);
        OpenPanelOnce = true;
        shownOnce = true;
    }
    IEnumerator fadeCharacterImage(int startAlpha, int targetAlpha)
    {
        float currentTime = 0f;
        Color charImage = CharacterImage.color;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            charImage.a = Mathf.Lerp(startAlpha, targetAlpha, currentTime / fadeDuration);
            CharacterImage.color = charImage;
            yield return null;
        }
    }

}
