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

    private void Start()
    {
        PlayerPrefs.SetInt("ChoosedChararcterValue",0);
    }

    private void Update()
    {
        if(string.IsNullOrEmpty(inputField.text))
        {
            fieldIsEmpty = true;
            generalMessageText.text = "To begin your scientist journey enter your name.";
        }
        if((inputField.text.Length>0) &&(inputField.text.Length<=6))
        {
            nameIsTooShort = true;
            generalMessageText.text = "Your name should contains atleast 6 characters.";
        }
        if(inputField.text.Length>=7 && inputField.text!="")
        {
            nameIsTooShort = false;
            fieldIsEmpty = false;
        }
        if(PlayerPrefs.GetInt("ChoosedChararcterValue") ==0)
        {
            characterNotChoosen = true;
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
    }
    public void StartPlaying()
    {
        if(fieldIsEmpty)
        {
            StartCoroutine(SendGeneralMessage());
        }
        if(nameIsTooShort)
        {
            StartCoroutine(SendGeneralMessage());
        }
        if(characterNotChoosen)
        {
            StartCoroutine(SendGeneralMessage());
        }
        if(!fieldIsEmpty && !nameIsTooShort && !characterNotChoosen)
        {
            OnboardingScreen.GetComponent<Animator>().SetBool("IsOpen", false);
            PlayerPrefs.SetInt("OnboardingDone", 1);
            StartCoroutine(changeScene());
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
