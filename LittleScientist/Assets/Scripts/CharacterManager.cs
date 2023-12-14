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
    public int fadeDuration=4;
    public string loadedString;
    public bool OpenPanelOnce = false;
    public bool shownOnce = false;

    private Coroutine messageCoroutine;

    [Header("Instances")]
    public combinationManager combManager;
    private void Start()
    {
        combManager = FindObjectOfType<combinationManager>();

    }

    // Update is called once per frame
    void Update()
    {
        loadedString = combManager.loadedString;
    }

    public void HandlingCharacterBehaviour()
    {
        switch (combManager.currentElementState)
        {
            case combinationManager.ElementState.InitialState:
                ShowMessage("Hello Let's find new elements.");
                Debug.Log("State is-Initial");

                break;

            case combinationManager.ElementState.NewElementFound:
                ShowMessage("Wow! New Element is Found.");
                Debug.Log("State is-New Element Found");

                break;
            case combinationManager.ElementState.ElementExists:
                ShowMessage("Hmm! The element already exists.");
                Debug.Log("State is-Element Exists");

                break;
            case combinationManager.ElementState.NoCombinationFound:
                ShowMessage("Hmm! I can't find any combination of these elements.");
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


    IEnumerator DisplayMessage(string message)
    {
        CharacterPanel.SetActive(true);
        MessageBoxText.text = message;
        yield return new WaitForSeconds(2f);
        //CharacterPanel.SetActive(false);
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
