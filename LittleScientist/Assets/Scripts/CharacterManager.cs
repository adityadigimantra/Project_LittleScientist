using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [Header("Character Fields")]
    public GameObject CharacterPanel;
    public Image CharacterImage;
    public Image CharacterBgImage;
    public Image MessageBoxImage;
    public Text MessageBoxText;
    public int fadeDuration=4;

    public bool OpenPanelOnce = false;
    public bool shownOnce = false;

    [Header("Instances")]
    public combinationManager combManager;
    private void Start()
    {
        combManager = FindObjectOfType<combinationManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void HandlingCharacterBehaviour()
    {
        switch (combManager.currentElementState)
        {
            case combinationManager.ElementState.InitialState:
                MessageBoxText.text = "Hello Let's find new elements.";
                Debug.Log("State is-Initial");
                if(!shownOnce)
                {

                    StartCoroutine(switchOnOffCharacterPanel());
                }
                break;

            case combinationManager.ElementState.NewElementFound:
                MessageBoxText.text = "Wow! New Element Found.";
                Debug.Log("State is-New Element Found");
                if (!OpenPanelOnce)
                {
                    StartCoroutine(switchOnOffCharacterPanel());
                    

                }
                break;
            case combinationManager.ElementState.ElementExists:
                MessageBoxText.text = "Hmm! This element already exists.";
                Debug.Log("State is-Element Exists");
                if (!OpenPanelOnce)
                {
                    StartCoroutine(switchOnOffCharacterPanel());
                    
                }
                break;
            case combinationManager.ElementState.NoCombinationFound:
                MessageBoxText.text = "Hmm! I can't find any combination of these elements.";
                Debug.Log("State is-No Combination Found");
                if(!OpenPanelOnce)
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



        //yield return fadeCharacterImage(0, 1);
        //yield return fadeCharacterImage(1,0);
        IEnumerator fadeCharacterImage(int startAlpha,int targetAlpha)
        {
            float currentTime = 0f;
            Color charImage = CharacterImage.color;
            while(currentTime<fadeDuration)
            {
                currentTime += Time.deltaTime;
                charImage.a = Mathf.Lerp(startAlpha, targetAlpha, currentTime / fadeDuration);
                CharacterImage.color = charImage;
                yield return null;
            }
        }

    }
}
