using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreText;
    public Text ElementDiscoveredText;
    public Text TotalElementsText;
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
            ScoreText.text = combinationManager.FinalScore.ToString();
            ElementDiscoveredText.text = combinationManager.elementDiscoveredCount.ToString();
            TotalElementsText.text = "/"+ combinationManager.TotalElements.ToString();
        }
    }
}
