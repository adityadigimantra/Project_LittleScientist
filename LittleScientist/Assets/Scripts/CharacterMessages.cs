using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMessages : MonoBehaviour
{
    [Header("Inactivity Messages")]
    public string[] inactivityMessages;

    [Header("Inactivity Messages-Return")]
    public string[] inactivityMessagesOnReturn;

    [Header("Welcoming Messages")]
    public string[] welcomingMessages;

    [Header("New Elment Found Messages")]
    public string[] newElementFoundMessages;

    [Header("Combination Exists Messages")]
    public string[] combinationExistsMessages;

    [Header("No Combination Exists Messages")]
    public string[] noCombinationExistsMessages;

    [Header("Random Fun Facts Messages")]
    public string[] randomFunFactsMessages;
    [Header("Tutorial Messages")]
    public string[] TutorialMessages;
    [Header("ElementDragging Messages")]
    public string[] ElementDraggingMessage;

    public string ReturnInactivityMessages()
    {
        int randomIndex = Random.Range(0, inactivityMessages.Length);
        return inactivityMessages[randomIndex];
    }

    public string ReturnInactivityMessagesOnReturn()
    {
        int randomIndex = Random.Range(0, inactivityMessagesOnReturn.Length);
        return inactivityMessagesOnReturn[randomIndex];
    }

    public string ReturnWelcomingMessages()
    {
        int randomIndex = Random.Range(0, welcomingMessages.Length);
        return welcomingMessages[randomIndex];
    }

    public string ReturnNewElementFoundMessages()
    {
        int randomIndex = Random.Range(0, newElementFoundMessages.Length);
        return newElementFoundMessages[randomIndex];
    }

    public string ReturnCombinationExistsMessages()
    {
        int randomIndex = Random.Range(0, combinationExistsMessages.Length);
        return combinationExistsMessages[randomIndex];
    }

    public string ReturnNoCombinationExistsMessages()
    {
        int randomIndex = Random.Range(0, noCombinationExistsMessages.Length);
        return noCombinationExistsMessages[randomIndex];
    }

    public string ReturnFunFactsMessages()
    {
        int randomIndex = Random.Range(0, randomFunFactsMessages.Length);
        return randomFunFactsMessages[randomIndex];
    }

    //Tutorial Messages

    public string ReturnTutorialIntroMessage()
    {
        return TutorialMessages[0];
    }
    public string ReturnTutorialMessageForEarthElement()
    {
        return TutorialMessages[1];
    }
    public string ReturnTutorialMessageForWaterElement()
    {
        return TutorialMessages[2];
    }
    public string ReturnTutorialMessageForAirElement()
    {
        return TutorialMessages[4];
    }
    public string ReturnTutorialMessageForFireElement()
    {
        return TutorialMessages[3];
    }
    public string ReturnTutorialMessageForTrashElement()
    {
        return TutorialMessages[5];
    }
    public string ReturnTutorialMessageForSettingElement()
    {
        return TutorialMessages[6];
    }
    public string ReturnTutorialMessageForCleanUpElement()
    {
        return TutorialMessages[7];
    }
    public string ReturnTutorialMessageForEncylopedia()
    {
        return TutorialMessages[8];
    }
    public string ReturnTutorialMessageForHints()
    {
        return TutorialMessages[9];
    }
    public string ReturnTutorialMessageForDiscoveryTray()
    {
        return TutorialMessages[10];
    }
    public string ReturnTutorialMessageForDraggingFirstElement()
    {
        return ElementDraggingMessage[0];
    }
    public string ReturnTutorialMessageForDraggingSecondElement()
    {
        return ElementDraggingMessage[1];
    }
    public string ReturnTutorialMessageForDraggingElementIntoEachOther()
    {
        return ElementDraggingMessage[2];
    }
    public string ReturnTutorialMessageForNewElementCreated()
    {
        return ElementDraggingMessage[3];
    }
    public string ReturnTutorialMessageForMoveToTrash()
    {
        return ElementDraggingMessage[6];
    }
    public string ReturnTutorialMessageForConclusion()
    {
        return ElementDraggingMessage[4];
    }
    public string ReturnTutorialMessageForTutorialComplete()
    {
        return ElementDraggingMessage[5];
    }

}
