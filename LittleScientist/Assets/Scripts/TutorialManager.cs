using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Instances")]
    public combinationManager comManager;
    public CharacterManager charManager;
    public CharacterMessages charMessages;
    public SoundManager soundManager;


    [Header("General Items")]
    public GameObject FirstHand;
    public GameObject SecondHand;
    public GameObject TutorialPanel;

    [Header("4-Elements")]
    public GameObject[] startingFourElements;
    public GameObject[] elementsBackgrounds;


    void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        charManager = FindObjectOfType<CharacterManager>();
        charMessages = FindObjectOfType<CharacterMessages>();
        soundManager = FindObjectOfType<SoundManager>();


        if (PlayerPrefs.GetInt("ShownTutorial") == 0) //If Tutorial not shown to the user.
        {
           
            StartCoroutine(IntroduceElements());
        }
    }

    IEnumerator IntroduceElements()
    {
        TutorialPanel.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        comManager.GiveWelcomeMessage();
        yield return new WaitForSeconds(3f);
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(1.2f);
        //Character gives small Introduction.
        CharacterGivesSmallIntro();
        yield return new WaitForSeconds(2.5f);
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(1.3f);
        //Character Introduces Earth Element.
        CharacterIntroducesEarthElement();
        yield return new WaitForSeconds(4f);
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(1.2f);
        CharacterIntroducesWaterElement();
        yield return new WaitForSeconds(4f);
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(1.2f);
        CharacterIntroducesFireElement();
        yield return new WaitForSeconds(4f);
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(1.2f);
        CharacterIntroducesAirElement();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(1.2f);
        

    }
   
    // Update is called once per frame
    void Update()
    {
    }

    public void CharacterGivesSmallIntro()
    {
        string message = charMessages.ReturnTutorialIntroMessage();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
    }
    public void CharacterIntroducesEarthElement()
    {
        string message = charMessages.ReturnTutorialMessageForEarthElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
        startingFourElements[0].transform.GetChild(3).gameObject.SetActive(true);
    }
    public void CharacterIntroducesWaterElement()
    {
        string message = charMessages.ReturnTutorialMessageForWaterElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
        startingFourElements[1].transform.GetChild(3).gameObject.SetActive(true);
    }
    public void CharacterIntroducesFireElement()
    {
        string message = charMessages.ReturnTutorialMessageForFireElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
        startingFourElements[2].transform.GetChild(3).gameObject.SetActive(true);
    }
    public void CharacterIntroducesAirElement()
    {
        string message = charMessages.ReturnTutorialMessageForAirElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
        startingFourElements[3].transform.GetChild(3).gameObject.SetActive(true);
    }
    public void SendTutorialMessageForHandOne()
    {
        //string messageForHandOne = charMessages.ReturnTutorialMessageForHandOne();
        //charManager.HandlingCharacterBehaviour(messageForHandOne, 20);
        FirstHand.SetActive(true);
        TutorialPanel.SetActive(false);
        soundManager.PlayCharacterWelcomingSound();

    }
    public void CloseHandOneTutorialAfterFewSec()
    {
        FirstHand.GetComponent<Animator>().SetBool("CloseHand1", true);
    }
    public void SendTutorialMessageForHandTwo()
    {
        //string messageForHandOne = charMessages.ReturnTutorialMessageForHandTwo();
        //charManager.HandlingCharacterBehaviour(messageForHandOne, 20);
        soundManager.PlayCharacterWelcomingSound();
    }
}
