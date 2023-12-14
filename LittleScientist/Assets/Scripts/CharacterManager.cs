using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public GameObject CharacterPanel;
    public Text MessageBoxText;
    public combinationManager comb_instance;
    public bool OpenPanelOnce = false;
    public bool shownOnce = false;
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandlingCharacterBehaviour();
    }

    public void HandlingCharacterBehaviour()
    {
        switch (comb_instance.currentElementState)
        {
            case combinationManager.ElementState.InitialState:
                MessageBoxText.text = "Hello Let's find new elements";
                Debug.Log("State is-Initial");
                if(!shownOnce)
                {
                    StartCoroutine(switchOnOffCharacterPanel());
                }
                break;

            case combinationManager.ElementState.NewElementFound:
                MessageBoxText.text = "Wow! New Element Found";
                Debug.Log("State is-New Element Found");
                if (!OpenPanelOnce)
                {
                    StartCoroutine(switchOnOffCharacterPanel());
                }
                break;
            case combinationManager.ElementState.ElementExists:
                MessageBoxText.text = "Hmm! This element already exists";
                Debug.Log("State is-Element Exists");
                if (!OpenPanelOnce)
                {
                    StartCoroutine(switchOnOffCharacterPanel());
                }
                break;
        }

        IEnumerator switchOnOffCharacterPanel()
        {
            CharacterPanel.SetActive(true);
            yield return new WaitForSeconds(4f);
            CharacterPanel.SetActive(false);
            OpenPanelOnce = true;
            shownOnce = true;
        }

    }
}
