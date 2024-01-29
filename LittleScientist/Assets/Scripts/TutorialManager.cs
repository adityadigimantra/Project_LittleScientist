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
    public PlayerActivityMonitor playerActivityMonitor;

    [Header("General Items")]
    public GameObject FirstHand;
    public GameObject SecondHand;
    public GameObject TutorialPanel;
    public float TimeHoldMessage;
    public float TimeCloseMessage;

    [Header("4-Elements")]
    public GameObject[] startingFourElements;
    public GameObject[] elementsBackgrounds;
    public GameObject[] otherElements;


    void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        charManager = FindObjectOfType<CharacterManager>();
        charMessages = FindObjectOfType<CharacterMessages>();
        soundManager = FindObjectOfType<SoundManager>();
        playerActivityMonitor = FindObjectOfType<PlayerActivityMonitor>();

        if (PlayerPrefs.GetInt("ShownTutorial") == 0) //If Tutorial not shown to the user.
        {
            playerActivityMonitor.enabled = false;
            StartCoroutine(IntroduceElements());
        }
    }

    IEnumerator IntroduceElements()
    {
        TutorialPanel.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        //Section-1
        comManager.GiveWelcomeMessage();
        yield return new WaitForSeconds(TimeHoldMessage);
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(2f);

        //Section-2
        //Character gives small Introduction.
        CharacterGivesSmallIntro();
        yield return new WaitForSeconds(TimeHoldMessage);
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);

        //Section-3
        //Character Introduces Earth Element.
        CharacterIntroducesEarthElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        CloseEarthElementsAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);

        //Section-4
        //Character Introduces Water Element.
        CharacterIntroducesWaterElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        closeWaterElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);

        //Section-5
        //Character Introduces Fire Element.
        CharacterIntroducesFireElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        CloseFireElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);

        //Section-6
        //Character Introduces Air Element.
        CharacterIntroducesAirElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        CloseAirElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);


        //Section-7
        //Character Introduces Trash Element.
        CharacterIntroducesTrashElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        CloseTrashElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);

        //Section-8
        //Character Introduces Bottom Elements.
        CharacterIntroducesSettingElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        CloseSettingElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);

        //Section-9
        playerActivityMonitor.enabled = true;
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

    #region EarthElement and Background data
    public void CharacterIntroducesEarthElement()
    {
        string message = charMessages.ReturnTutorialMessageForEarthElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();

        //Earth Element Circle and Background index=0
        elementsBackgrounds[0].gameObject.SetActive(true);
        elementsBackgrounds[0].gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
        startingFourElements[0].transform.GetChild(3).gameObject.SetActive(true);
        startingFourElements[0].transform.GetChild(3).gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
       
    }
    public void CloseEarthElementsAnimations()
    {
        startingFourElements[0].transform.GetChild(3).gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
        elementsBackgrounds[0].gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
    }
    #endregion

    #region WaterElement and Background data
    public void CharacterIntroducesWaterElement()
    {
        string message = charMessages.ReturnTutorialMessageForWaterElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();

        //Water Element Circle and Background index=1
        elementsBackgrounds[0].SetActive(false);
        elementsBackgrounds[1].gameObject.SetActive(true);
        elementsBackgrounds[1].gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
        startingFourElements[1].transform.GetChild(3).gameObject.SetActive(true);
        startingFourElements[1].transform.GetChild(3).gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
    }
    public void closeWaterElementAnimations()
    {
        elementsBackgrounds[1].gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
        startingFourElements[1].transform.GetChild(3).gameObject.GetComponent<Animator>().SetBool("IsOpen", false);

    }
    #endregion

    #region FireElement and Background Data
    public void CharacterIntroducesFireElement()
    {
        string message = charMessages.ReturnTutorialMessageForFireElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();

        elementsBackgrounds[1].SetActive(false);
        elementsBackgrounds[2].gameObject.SetActive(true);
        elementsBackgrounds[2].gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
        startingFourElements[2].transform.GetChild(3).gameObject.SetActive(true);
        startingFourElements[2].transform.GetChild(3).gameObject.GetComponent<Animator>()  .SetBool("IsOpen", true);
    }

    public void CloseFireElementAnimations()
    {
        elementsBackgrounds[2].gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
        startingFourElements[2].transform.GetChild(3).gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
    }

    #endregion

    #region AirElement and Background Data
    public void CharacterIntroducesAirElement()
    {
        string message = charMessages.ReturnTutorialMessageForAirElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();

        elementsBackgrounds[2].SetActive(false);
        elementsBackgrounds[3].gameObject.SetActive(true);
        elementsBackgrounds[3].gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
        startingFourElements[3].transform.GetChild(3).gameObject.SetActive(true);
        startingFourElements[3].transform.GetChild(3).gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
    }
    public void CloseAirElementAnimations()
    {
        elementsBackgrounds[3].gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
        startingFourElements[3].transform.GetChild(3).gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
    }
    #endregion

    #region Trash and Background Data
    public void CharacterIntroducesTrashElement()
    {
        string message = charMessages.ReturnTutorialMessageForTrashElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();

        elementsBackgrounds[3].SetActive(false);
        elementsBackgrounds[4].SetActive(true);
        elementsBackgrounds[4].gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
        otherElements[0].transform.GetChild(0).gameObject.SetActive(true);
        otherElements[0].transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("IsOpen", true);

    }

    public void CloseTrashElementAnimations()
    {
        elementsBackgrounds[4].gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
        otherElements[0].transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
    }


    #endregion

    #region Setting and Background Data
    public void CharacterIntroducesSettingElement()
    {
        string message = charMessages.ReturnTutorialMessageForSettingElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
        elementsBackgrounds[4].SetActive(false);
        elementsBackgrounds[5].SetActive(true);
        elementsBackgrounds[5].gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
        otherElements[1].transform.GetChild(0).gameObject.SetActive(true);
        otherElements[1].transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("IsOpen", true);

    }

    public void CloseSettingElementAnimations()
    {
        elementsBackgrounds[5].gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
        otherElements[1].transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
    }
    #endregion



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
