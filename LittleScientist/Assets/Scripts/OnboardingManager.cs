using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnboardingManager : MonoBehaviour
{
    public GameObject OnboardingScreen;
    public InputField inputField;
    public GameObject[] boyObj;
    public GameObject[] girlObj;
    public GameObject playButton;
    public Text generalMessageText;

    public bool fieldIsEmpty = false;
    public bool nameIsTooShort = false;
    public bool characterNotChoosen = false;

    [Header("Onboarding Items")]
    public Button nextButton;
    public Button startButton;
    public GameObject NamePanel;
    public GameObject characterPanel;
    

    private void Start()
    {
        PlayerPrefs.SetInt("ChoosedChararcterValue",0);
       

    }

    private void Update()
    {
        string filteredText = "";
        inputField.text = inputField.text.Replace(" ", "");
        //foreach(char c in inputField.text)
        //{
        //    if(c!='@' && c!='#' && c != '$' && c != '!' && c != '%' && c != '^' && c != '&' && c != '*' && c != '(' && c != ')' && c != '-' && c != '_' && c != '+' && c != '=')
        //    {
        //        filteredText += c;
        //    }
        //}
        //inputField.text = filteredText;

    }

    public void InputfieldCallbacks()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            fieldIsEmpty = true;
            generalMessageText.text = "To begin your scientist journey enter your name.";
            //nextButton.interactable = false;
        }
        if ((inputField.text.Length > 0) && (inputField.text.Length <= 6))
        {
            nameIsTooShort = true;
            generalMessageText.text = "Your name should contains atleast 6 characters.";
            //nextButton.interactable = false;
        }
        if (inputField.text.Length >= 6 && inputField.text != "")
        {
            nameIsTooShort = false;
            fieldIsEmpty = false;
            //nextButton.interactable = true;
        }
    }

    public void CharacterCallbacks()
    {
        if (PlayerPrefs.GetInt("ChoosedChararcterValue") == 0)
        { 
            generalMessageText.text = "Please choose any character.";
        }
    }
    public void ChooseBoyCharacter()
    {
        string character = "boy";
        PlayerPrefs.SetString("CharacterChoosen", character);
        PlayerPrefs.SetInt("ChoosedChararcterValue", 1);
        boyObj[0].GetComponent<Image>().enabled = false;
        boyObj[1].SetActive(true);
        characterNotChoosen = false;
        //Girl to turn off selected.
        girlObj[0].GetComponent<Image>().enabled = true;
        girlObj[1].SetActive(false);
        //startButton.interactable = true;
    }

    public void ChooseGirlCharacter()
    {
        string character = "girl";
        PlayerPrefs.SetString("CharacterChoosen", character);
        PlayerPrefs.SetInt("ChoosedChararcterValue", 1);
        girlObj[0].GetComponent<Image>().enabled = false;
        girlObj[1].SetActive(true);
        characterNotChoosen = false;
        //Boy to turn off selected
        boyObj[0].GetComponent<Image>().enabled = true;
        boyObj[1].SetActive(false);
        characterNotChoosen = false;
        //startButton.interactable = true;
    }
    public void StartPlaying()
    {
        CharacterCallbacks();

        if(PlayerPrefs.GetInt("ChoosedChararcterValue")==0)
        {
            StartCoroutine(SendGeneralMessage());
        }

        if(PlayerPrefs.GetInt("ChoosedChararcterValue")==1)
        {
            OnboardingScreen.GetComponent<Animator>().SetBool("IsOpen", false);
            PlayerPrefs.SetInt("OnboardingDone", 1);
            StartCoroutine(changeScene());
        }
        
    }

    public void NextButtonFun()
    {
        InputfieldCallbacks();
        if (fieldIsEmpty)
        {
            StartCoroutine(SendGeneralMessage());
        }
        if (nameIsTooShort)
        {
            StartCoroutine(SendGeneralMessage());
        }
        if(!fieldIsEmpty && !nameIsTooShort)
        {
            //Need to save name if required.
            NamePanel.SetActive(false);
            characterPanel.SetActive(true);
        }
    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(3f);
        OnboardingScreen.SetActive(false);
        SceneManager.LoadScene(2);
    }
    IEnumerator SendGeneralMessage()
    {
        generalMessageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        generalMessageText.gameObject.SetActive(false);
    }
}
