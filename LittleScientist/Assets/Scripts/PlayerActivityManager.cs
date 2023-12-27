using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivityManager : MonoBehaviour
{
    [SerializeField]
    private bool isPlayerActive =false;
    [SerializeField]
    private float inactivityTimer = 0f;
    [SerializeField]
    private float inactivityThreshold = 10f;
    [SerializeField]
    private bool hasDisplayedMessage = false;


    [Header("Instances")]
    public combinationManager comManager;
    public CharacterManager charManager;
    void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        charManager = FindObjectOfType<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
        MakePlayerActive();
        MakePlayerInActive();
        
    }

    public void MakePlayerActive()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ActivatePlayer();
        }
        if (Input.touchCount > 0)
        {
            ActivatePlayer();
        }
    }
    public void MakePlayerInActive()
    {
        if (inactivityTimer >= inactivityThreshold)
        {
            isPlayerActive = false;
            comManager.currentElementState = combinationManager.ElementState.IdleState;
            if(!hasDisplayedMessage)
            {

                hasDisplayedMessage = true;
            }
        }

    }



    public void UpdateInactivityTimer()
    {
        if (!isPlayerActive)
        {
            inactivityTimer += Time.deltaTime;
            

        }
        else
        {
           
            comManager.currentElementState = combinationManager.ElementState.InitialState;
        }
    }

    public void ActivatePlayer()
    {
        isPlayerActive = true;
        inactivityTimer = 0;
    }

    public void StartTimer()
    {
        inactivityTimer += Time.deltaTime;
    }

}
