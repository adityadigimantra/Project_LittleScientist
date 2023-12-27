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
}
