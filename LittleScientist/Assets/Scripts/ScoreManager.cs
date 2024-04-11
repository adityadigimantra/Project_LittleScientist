using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("Themes Scores Text")]
    public GameObject def_ScorePanel;
    public Text def_ScoreText;
    public GameObject for_ScorePanel;
    public Text for_ScoreText;
    public GameObject aqua_ScorePanel;
    public Text aqua_ScoreText;

    [Header("Def_Discovery Tray Data")]
    public GameObject def_discoveryTray;
    public Text def_ElementDiscoveredText;
    public Text def_TotalElementsText;

    [Header("for_Discovery Tray Data")]
    public GameObject for_discoveryTray;
    public Text for_ElementDiscoveredText;
    public Text for_TotalElementsText;

    [Header("aqua_Discovery Tray Data")]
    public GameObject aqua_discoveryTray;
    public Text aqua_ElementDiscoveredText;
    public Text aqua_TotalElementsText;

    public combinationManager combinationManager;


    // Start is called before the first frame update
    void Start()
    {
        combinationManager = FindObjectOfType<combinationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (combinationManager != null)
        {
            if(def_ScorePanel.activeSelf)
            {
                def_ScoreText.text = combinationManager.FinalScore.ToString();
            }
            if (for_ScorePanel.activeSelf)
            {
                for_ScoreText.text = combinationManager.FinalScore.ToString();
            }
            if (aqua_ScorePanel.activeSelf)
            {
                aqua_ScoreText.text = combinationManager.FinalScore.ToString();
            }



            if (def_discoveryTray.activeSelf)
            {
                def_ElementDiscoveredText.text = combinationManager.elementDiscoveredCount.ToString();
                def_TotalElementsText.text = "/" + combinationManager.TotalElements.ToString();
            }
            if(for_discoveryTray.activeSelf)
            {
                for_ElementDiscoveredText.text = combinationManager.elementDiscoveredCount.ToString();
                for_TotalElementsText.text = "/" + combinationManager.TotalElements.ToString();
            }
            if(aqua_discoveryTray.activeSelf)
            {
                aqua_ElementDiscoveredText.text = combinationManager.elementDiscoveredCount.ToString();
                aqua_TotalElementsText.text = "/" + combinationManager.TotalElements.ToString();
            }
            
        }
    }
}
