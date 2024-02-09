using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("Instances")]
    public combinationManager comManager;
    public CharacterManager charManager;
    public CharacterMessages charMessages;
    public SoundManager soundManager;
    public SettingsManager settingsManager;
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

    [Header("TutorialElements")]
    public GameObject GamePlaySection;
    public GameObject[] tutorialElements;
    public GameObject newElementCreatedPanel;
    public GameObject tutorialCompletePanel;
    public GameObject fadedImage;

    [Header("Buttons")]
    public Button skipButton;
    public Button nextButton;
    public GameObject restartButton;
    void Start()
    {
        #region Getting Instances
        comManager = FindObjectOfType<combinationManager>();
        charManager = FindObjectOfType<CharacterManager>();
        charMessages = FindObjectOfType<CharacterMessages>();
        soundManager = FindObjectOfType<SoundManager>();
        playerActivityMonitor = FindObjectOfType<PlayerActivityMonitor>();
        settingsManager = FindObjectOfType<SettingsManager>();
        #endregion

        #region Switch For Tutorial using PlayerPrefs
        if (PlayerPrefs.GetInt("ShownTutorial") == 0) //If Tutorial not shown to the user.
        {
            TutorialPanel.SetActive(true);
            playerActivityMonitor.enabled = false;
            settingsManager.TurnOnMusic();
            settingsManager.TurnOnSound();
            StartCoroutine(IntroduceElements());
        }
        else
        {
            TutorialPanel.SetActive(false);
            restartButton.SetActive(true);
        }
        #endregion
    }

    IEnumerator IntroduceElements()
    {


        #region Section1 (Character Gives Welcome Message)
        //Section-1
        
        yield return new WaitForSeconds(0.2f);
        fadedImage.SetActive(true);
        fadedImage.GetComponent<Animator>().SetBool("IsOpen", true);
        comManager.GiveWelcomeMessage();
        yield return new WaitForSeconds(TimeHoldMessage);
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(2f);
        #endregion

        #region Section-2 (Character Gives Small Introduction)
        //Section-2
        //Character gives small Introduction.
        CharacterGivesSmallIntro();
        yield return new WaitForSeconds(TimeHoldMessage);
        charManager.CloseCurrentMessage();
        fadedImage.GetComponent<Animator>().SetBool("IsOpen", false);
        yield return new WaitForSeconds(TimeCloseMessage);
        #endregion

        #region Section-3 (Character Introduces Earth)
        //Section-3
        //Character Introduces Earth Element.
        CharacterIntroducesEarthElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        fadedImage.SetActive(false);
        CloseEarthElementsAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);
        #endregion

        #region Section-4 (Character Introduces Water)
        //Section-4
        //Character Introduces Water Element.
        CharacterIntroducesWaterElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        closeWaterElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);
        #endregion

        #region Section-5 (Character Introduces Fire)
        //Section-5
        //Character Introduces Fire Element.
        CharacterIntroducesFireElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        CloseFireElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);
        #endregion

        #region Section-6 (Character Introduces Air)
        //Section-6
        //Character Introduces Air Element.
        CharacterIntroducesAirElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        CloseAirElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);
        #endregion

        #region Section-7 (Character Introduces Trash)
        //Section-7
        //Character Introduces Trash Element.
        CharacterIntroducesTrashElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        CloseTrashElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);
        #endregion

        #region Section-8 (Character Introduces Bottom Icons in Tray)
        //Section-8
        //Character Introduces Bottom Elements.
        CharacterIntroducesSettingElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        CloseSettingElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);
        

        //Section-9
        //Character Introduces CleanUp Element.
        CharacterIntroducesCleanUpElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        closeCleanUpElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);

        //Section-10
        //Character Introduces Encyclopedia Element.
        CharacterIntroducesEncyclopediaElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        CloseEncylopediaElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);

        //Section-11
        //Character Introduces Hints Element.
        CharacterIntroducesHintsElement();
        yield return new WaitForSeconds(TimeHoldMessage);
        CloseHintsElementAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);
        #endregion

        #region Section-9 (Character Introduces Discovery Tray)
        //Section-12
        //Character Introduces Discovery Tray Element.
        CharacterIntroducesDiscorveryTray();
        yield return new WaitForSeconds(TimeHoldMessage);
        CloseDiscoveryTrayAnimations();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(TimeCloseMessage);
        #endregion

        #region Section-10 (Character Introduces How To Make Elements)
        //Section-13
        //Character shows How to make elements.
        GamePlaySection.SetActive(true);
        CharacterGivesMessageToStartDraggingFirstElements();
        yield return new WaitForSeconds(3f);
        fadedImage.GetComponent<Animator>().SetBool("IsOpen", false);
        charManager.CloseCurrentMessage();
        CharacterDragsFirstElement();
        yield return new WaitForSeconds(2f);
        fadedImage.GetComponent<Animator>().SetBool("IsOpen", true);
        CharacterGivesMessageToStartDraggingSecondElements();
        yield return new WaitForSeconds(3f);
        fadedImage.GetComponent<Animator>().SetBool("IsOpen", false);
        charManager.CloseCurrentMessage();
        CharacterDragsSecondElement();
        yield return new WaitForSeconds(2);
        fadedImage.GetComponent<Animator>().SetBool("IsOpen", true);
        CharacterGivesMessageToStartDraggingElementsIntoEachOther();
        yield return new WaitForSeconds(3f);
        fadedImage.GetComponent<Animator>().SetBool("IsOpen", false);
        CharacterDragsSecondElementIntoFirst();
        yield return new WaitForSeconds(0.5f);//TODO-Fix
        OpenNewElementCreatedPanel();
        yield return new WaitForSeconds(4f);
        CloseNewElementCreatedPanel();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(1f);
        fadedImage.GetComponent<Animator>().SetBool("IsOpen", true);
        CharacterGivesMessageForNewElementCreated();
        yield return new WaitForSeconds(3f);
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(2f);
        CharacterGivesMessageForMovingToTrash();
        yield return new WaitForSeconds(3);
        CharacterDragsThirdElementToTrash();
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(1.5f);
        fadedImage.GetComponent<Animator>().SetBool("IsOpen", false);
        CharacterGivesMessageForConclusion();
        yield return new WaitForSeconds(3f);
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(2);
        OpenTutorialPanelComplete();
        CharacterGivesMessageForTutorialComplete();
        #endregion
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

    #region Cleanup and Background Data
    public void CharacterIntroducesCleanUpElement()
    {
        string message = charMessages.ReturnTutorialMessageForCleanUpElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();

        elementsBackgrounds[5].SetActive(false);
        elementsBackgrounds[6].SetActive(true);
        elementsBackgrounds[6].GetComponent<Animator>().SetBool("IsOpen", true);
        otherElements[2].transform.GetChild(0).gameObject.SetActive(true);
        otherElements[2].transform.GetChild(0).GetComponent<Animator>().SetBool("IsOpen", true);
    }
    public void closeCleanUpElementAnimations()
    {
        elementsBackgrounds[6].GetComponent<Animator>().SetBool("IsOpen", false);
        otherElements[2].transform.GetChild(0).GetComponent<Animator>().SetBool("IsOpen", false);
    }
    #endregion

    #region Encylopedia and Background Data
    public void CharacterIntroducesEncyclopediaElement()
    {
        string message = charMessages.ReturnTutorialMessageForEncylopedia();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();

        elementsBackgrounds[6].SetActive(false);
        elementsBackgrounds[7].SetActive(true);
        elementsBackgrounds[7].GetComponent<Animator>().SetBool("IsOpen", true);
        otherElements[3].transform.GetChild(0).gameObject.SetActive(true);
        otherElements[3].transform.GetChild(0).GetComponent<Animator>().SetBool("IsOpen", true);
    }

    public void CloseEncylopediaElementAnimations()
    {
        elementsBackgrounds[7].GetComponent<Animator>().SetBool("IsOpen", false);
        otherElements[3].transform.GetChild(0).GetComponent<Animator>().SetBool("IsOpen", false);
    }
    #endregion

    #region Hints and Background Data
    public void CharacterIntroducesHintsElement()
    {
        string message = charMessages.ReturnTutorialMessageForHints();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();

        elementsBackgrounds[7].SetActive(false);
        elementsBackgrounds[8].SetActive(true);
        elementsBackgrounds[8].GetComponent<Animator>().SetBool("IsOpen", true);
        otherElements[4].transform.GetChild(0).gameObject.SetActive(true);
        otherElements[4].transform.GetChild(0).GetComponent<Animator>().SetBool("IsOpen", true);
    }

    public void CloseHintsElementAnimations()
    {
        elementsBackgrounds[8].GetComponent<Animator>().SetBool("IsOpen", false);
        otherElements[4].transform.GetChild(0).GetComponent<Animator>().SetBool("IsOpen", false);
    }
    #endregion

    #region Discovery Tray and Background Data
    public void CharacterIntroducesDiscorveryTray()
    {
        string message = charMessages.ReturnTutorialMessageForDiscoveryTray();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();

        elementsBackgrounds[8].SetActive(false);
        otherElements[5].SetActive(true);
        otherElements[5].GetComponent<Animator>().SetBool("IsOpen", true);

    }
    public void CloseDiscoveryTrayAnimations()
    {
        otherElements[5].GetComponent<Animator>().SetBool("IsOpen", false);
        
    }
    #endregion

    #region GamePlay Collision for Tutorial
    public void CharacterGivesMessageToStartDraggingFirstElements()
    {
        fadedImage.SetActive(true);
        fadedImage.GetComponent<Animator>().SetBool("IsOpen", true);
        string message = charMessages.ReturnTutorialMessageForDraggingFirstElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
    }
    public void CharacterDragsFirstElement()
    {
        tutorialElements[0].SetActive(true);
        tutorialElements[0].GetComponent<Animator>().SetBool("SlideDown", true);
    }
    public void CharacterGivesMessageToStartDraggingSecondElements()
    {
        string message = charMessages.ReturnTutorialMessageForDraggingSecondElement();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
    }
    public void CharacterDragsSecondElement()
    {
        tutorialElements[1].SetActive(true);
        tutorialElements[1].GetComponent<Animator>().SetBool("SlideDown", true);
    }
    public void CharacterGivesMessageToStartDraggingElementsIntoEachOther()
    {
        string message = charMessages.ReturnTutorialMessageForDraggingElementIntoEachOther();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
    }
    public void CharacterDragsSecondElementIntoFirst()
    {
        tutorialElements[1].GetComponent<Animator>().SetBool("SlideToEarth", true);
    }
    public void OpenNewElementCreatedPanel()
    {
        newElementCreatedPanel.SetActive(true);
        newElementCreatedPanel.GetComponent<Animator>().SetBool("IsOpen", true);
        tutorialElements[0].SetActive(false);
        tutorialElements[1].SetActive(false);

    }
    public void CloseNewElementCreatedPanel()
    {
        newElementCreatedPanel.GetComponent<Animator>().SetBool("IsOpen", false);
        tutorialElements[2].SetActive(true);
    }


    public void CharacterGivesMessageForNewElementCreated()
    {
        string message = charMessages.ReturnTutorialMessageForNewElementCreated();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
    }

    public void CharacterGivesMessageForMovingToTrash()
    {
        string message = charMessages.ReturnTutorialMessageForMoveToTrash();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
    }
    public void CharacterDragsThirdElementToTrash()
    {
        tutorialElements[2].GetComponent<Animator>().SetBool("SlideToTrash", true);
    }
    public void CharacterGivesMessageForConclusion()
    {
        string message = charMessages.ReturnTutorialMessageForConclusion();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
    }
    public void OpenTutorialPanelComplete()
    {
        tutorialCompletePanel.SetActive(true);
        tutorialCompletePanel.GetComponent<Animator>().SetBool("IsOpen", true);
    }
    public void CloseTutorialPanelComplete()
    {
        //tutorialCompletePanel.SetActive(true);
        tutorialCompletePanel.GetComponent<Animator>().SetBool("IsOpen", false);
        charManager.CloseCurrentMessage();
        PlayerPrefs.SetInt("ShownTutorial", 1);
        TutorialPanel.SetActive(false);
        playerActivityMonitor.enabled = true;
    }
    public void CharacterGivesMessageForTutorialComplete()
    {
        string message = charMessages.ReturnTutorialMessageForTutorialComplete();
        charManager.HandlingCharacterBehaviour(message, 20);
        soundManager.PlayCharacterWelcomingSound();
    }
    #endregion


    #region Buttons Functions
    public void SkipTutorial()
    {
        StopAllCoroutines();
        charManager.CloseCurrentMessage();
        TutorialPanel.SetActive(false);
        restartButton.SetActive(true);
        PlayerPrefs.SetInt("ShownTutorial", 1);
        playerActivityMonitor.enabled = true;
        foreach(GameObject g in startingFourElements)
        {
            g.transform.GetChild(3).gameObject.SetActive(false);
        }
        foreach(GameObject g in elementsBackgrounds)
        {
            g.SetActive(false);
        }
        foreach(GameObject g in otherElements)
        {
            g.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    #endregion
}
