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

    public void HandlingCharacterBehaviour(string message,int time)
    {
        switch (combManager.currentElementState)
        {
            case combinationManager.ElementState.InitialState:
                ShowMessage("Let's Find New Elements");
                Debug.Log("State is-Initial");
                break;

            case combinationManager.ElementState.NewElementFound:
                string uppercaseWord = ConvertToUpperCase(message);
                ShowMessage("Wow! New Element "+uppercaseWord+" is Found.");
                Debug.Log("State is-New Element Found");
                break;
            case combinationManager.ElementState.ElementExists:
                ShowMessage(message);
                //StartCoroutine(givingDelay());
                Debug.Log("State is-Element Exists");
                break;
            case combinationManager.ElementState.NoCombinationFound:
                ShowMessage(message);
                //StartCoroutine(givingDelay());
                Debug.Log("State is-No Combination Found");
                break;
            case combinationManager.ElementState.IdleState:
                ShowMessage(message);
                Debug.Log("State is-Idle");
                break;
        }

        //yield return fadeCharacterImage(0, 1);
        //yield return fadeCharacterImage(1,0);
    }
    public void HandlingCharacterBehaviourdefault()
    {
        switch (combManager.currentElementState)
        {
            case combinationManager.ElementState.InitialState:
                ShowMessage("Let's Find New Elements");
                Debug.Log("State is-Initial");
                break;

            case combinationManager.ElementState.NewElementFound:

                ShowMessage("Wow! New Element is Found.");
                Debug.Log("State is-New Element Found");
                break;
            case combinationManager.ElementState.ElementExists:
                ShowMessage("Hmm! This Element already exists.");
                Debug.Log("State is-Element Exists");
                break;
            case combinationManager.ElementState.NoCombinationFound:
                ShowMessage("Oops! I cannot find any combination for these");
                Debug.Log("State is-No Combination Found");
                break;
        }
        //yield return fadeCharacterImage(0, 1);
        //yield return fadeCharacterImage(1,0);
    }
    public void ShowMessage(string message)
    {
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }
        messageCoroutine = StartCoroutine(DisplayMessage(message));
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
    IEnumerator DisplayMessage(string message)
    {
        CharacterPanel.SetActive(true);
        //messageBoxAnimator.SetBool("IsOpen", true);
        MessageBoxImage.gameObject.SetActive(true);
        MessageBoxText.text = message;
        yield return new WaitForSeconds(3f);
        messageBoxAnimator.SetBool("IsOpen", false);
        //MessageBoxImage.gameObject.SetActive(false);
        //CharacterPanel.SetActive(false);
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
