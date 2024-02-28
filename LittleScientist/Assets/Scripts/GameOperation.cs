using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;


public enum GameState
{
    Prepare,
    Playing,
    Paused,
    GameOver
}
public class GameOperation : MonoBehaviour
{
    [Header("Bools")]
    public bool CanStart = false;
    public bool gamePaused = false;
    public bool startPreparing = false;
    public static GameOperation _Instance;
    public static event System.Action<GameState,GameState> GameStateChanged;

    [Header("Panels")]
    public GameObject lowerSmallPanel;
    public GameObject SettingsPanel;
    public GameObject ParentSeachPanel;


    [Header("Discovery Trays")]
    public GameObject[] defaultDiscoveryTrayItems;
    public int def_discorveryTrayContentChildCount;
    public GameObject[] forestDiscoveryTrayItems;
    public int for_discorveryTrayContentChildCount;
    public GameObject[] aquaDiscoveryTrayItems;
    public int aqua_discorveryTrayContentChildCount;

    [Header("Theme Scroll Rects")]
    public GameObject def_BottomScrollRect;
    public int def_BottomElementsChildCount;
    public GameObject for_BottomScrollRect;
    public int for_BottomElementsChildCount;
    public GameObject aqua_BottomScrollRect;
    public int aqua_BottomElementsChildCount;

    [Header("Theme UI Controls")]
    public GameObject[] def_UIControls;
    public GameObject[] for_UIControls;
    public GameObject[] aqua_UIControls;

    [Header("GameBoard and UI Items")]
    public GameObject[] defaultThemeGameItems;
    public GameObject[] forestThemeGameItems;
    public GameObject[] AquaThemeGameItems;

    public GameObject DefaultMainObj;
    public GameObject ForestMainObj;
    public GameObject AquaMainObj;
    /// <summary>
    /// index 0=panel index 1=object index 2=opening button
    /// </summary>




    public string selectedTheme;


    public GameState GameState
    {
        get
        {
            return _gameState;
        }
        set
        {
            if(value!=_gameState)
            {
                GameState oldState = _gameState;
                _gameState = value;

                if(GameStateChanged!=null)
                {
                    GameStateChanged(_gameState, oldState);
                }
            }
        }
    }

    private GameState _gameState = GameState.Prepare;

    private void Start()
    {
        _Instance = new GameOperation();
        Debug.Log("Hello From GameOperation");
        ThemeApplyer();
    }

    public void ThemeApplyer()
    {
        selectedTheme = PlayerPrefs.GetString("Theme");
        if (string.IsNullOrEmpty(selectedTheme))
        {
            PlayerPrefs.SetString("Theme", "Default");
            selectedTheme = PlayerPrefs.GetString("Theme");
        }
        if (!string.IsNullOrEmpty(selectedTheme))
        {
            switch (selectedTheme)
            {
                case "Default":
                    foreach (GameObject g in defaultThemeGameItems)
                    {
                        g.SetActive(true);
                    }
                    foreach (GameObject g in forestThemeGameItems)
                    {
                        g.SetActive(false);
                    }
                    foreach (GameObject g in AquaThemeGameItems)
                    {
                        g.SetActive(false);
                    }
                    break;

                case "Forest":
                    foreach (GameObject g in defaultThemeGameItems)
                    {
                        g.SetActive(false);
                    }
                    foreach (GameObject g in forestThemeGameItems)
                    {
                        g.SetActive(true);
                    }
                    foreach (GameObject g in AquaThemeGameItems)
                    {
                        g.SetActive(false);
                    }
                    break;

                case "Aqua":
                    foreach (GameObject g in defaultThemeGameItems)
                    {
                        g.SetActive(false);
                    }
                    foreach (GameObject g in forestThemeGameItems)
                    {
                        g.SetActive(false);
                    }
                    foreach (GameObject g in AquaThemeGameItems)
                    {
                        g.SetActive(true);
                    }
                    break;
            }
        }
    }

    private void Update()
    {

        switch (selectedTheme)
        {
            case "Default":
                def_BottomElementsChildCount = def_BottomScrollRect.transform.childCount;
                if (def_BottomElementsChildCount >= 8)
                {
                    SwitchOnControls();
                }
                def_discorveryTrayContentChildCount = defaultDiscoveryTrayItems[3].transform.childCount;
                if (def_discorveryTrayContentChildCount >= 1)
                {
                    defaultDiscoveryTrayItems[4].SetActive(false);
                }
                else
                {
                    defaultDiscoveryTrayItems[4].SetActive(true);
                }
                break;


            case "Forest":
                for_BottomElementsChildCount = for_BottomScrollRect.transform.childCount;
                if (for_BottomElementsChildCount >= 8)
                {
                    SwitchOnControls();
                }
                for_discorveryTrayContentChildCount = forestDiscoveryTrayItems[3].transform.childCount;
                if (for_discorveryTrayContentChildCount >= 1)
                {
                    forestDiscoveryTrayItems[4].SetActive(false);
                }
                else
                {
                    forestDiscoveryTrayItems[4].SetActive(true);
                }
                break;

            case "Aqua":
                aqua_BottomElementsChildCount = aqua_BottomScrollRect.transform.childCount;
                if (aqua_BottomElementsChildCount >= 8)
                {
                    SwitchOnControls();
                }
                aqua_discorveryTrayContentChildCount = aquaDiscoveryTrayItems[3].transform.childCount;
                if (aqua_discorveryTrayContentChildCount >= 1)
                {
                    aquaDiscoveryTrayItems[4].SetActive(false);
                }
                else
                {
                    aquaDiscoveryTrayItems[4].SetActive(true);
                }
                break;
        }



        

    }
    public void SwitchOnControls()
    {
        ParentSeachPanel.SetActive(true);
        switch (selectedTheme)
        {
            case "Default":
                foreach (GameObject g in def_UIControls)
                {
                    g.SetActive(true);
                }
                break;

            case "Forest":
                foreach (GameObject g in for_UIControls)
                {
                    g.SetActive(true);
                }
                break;

            case "Aqua":
                foreach (GameObject g in aqua_UIControls)
                {
                    g.SetActive(true);
                }
                break;
        }

       
    }

    public void ChangeScene(int index)
    {
        GameOperation._Instance.GameState = GameState.Prepare;
        SceneManager.LoadScene(index);
        string key1 = "element1";
        string key2 = "element2";

        PlayerPrefs.DeleteKey(key1);
        PlayerPrefs.DeleteKey(key2);
        SettingsPanel.SetActive(false);
        PlayerPrefs.SetInt("IsRestart", 1);
        EmptyStringInComManager();
    }

    #region Discovery Trays
    public void OpenDiscoveryTray()
    {
        string selectedTheme = PlayerPrefs.GetString("Theme");
        switch(selectedTheme)
        {
            case "Default":
                defaultDiscoveryTrayItems[0].SetActive(true);//panel
                defaultDiscoveryTrayItems[0].GetComponent<Animator>().SetBool("IsOpen", true);//Panel
                defaultDiscoveryTrayItems[1].GetComponent<Animator>().SetBool("IsOpen", true);//Object
                defaultDiscoveryTrayItems[2].SetActive(false);//Discovery Tray Button
                break;

            case "Forest":
                forestDiscoveryTrayItems[0].SetActive(true);
                forestDiscoveryTrayItems[0].GetComponent<Animator>().SetBool("IsOpen", true);
                forestDiscoveryTrayItems[1].GetComponent<Animator>().SetBool("IsOpen", true);
                forestDiscoveryTrayItems[2].SetActive(false);//Discovery Tray Button
                break;

            case "Aqua":
                aquaDiscoveryTrayItems[0].SetActive(true);
                aquaDiscoveryTrayItems[0].GetComponent<Animator>().SetBool("IsOpen", true);
                aquaDiscoveryTrayItems[1].GetComponent<Animator>().SetBool("IsOpen", true);
                aquaDiscoveryTrayItems[2].SetActive(false);//Discovery Tray Button
                break;

        }
        

    }
    public void CloseDiscoveryPanel()
    {
        string selectedTheme = PlayerPrefs.GetString("Theme");
        switch (selectedTheme)
        {
            case "Default":
                defaultDiscoveryTrayItems[0].GetComponent<Animator>().SetBool("IsOpen", false);//Panel
                defaultDiscoveryTrayItems[1].GetComponent<Animator>().SetBool("IsOpen", false);
                StartCoroutine(def_delaythenClose());
                break;

            case "Forest":
                forestDiscoveryTrayItems[0].GetComponent<Animator>().SetBool("IsOpen", false);//Panel
                forestDiscoveryTrayItems[1].GetComponent<Animator>().SetBool("IsOpen", false);
                StartCoroutine(forest_delaythenClose());
                break;

            case "Aqua":
                aquaDiscoveryTrayItems[0].GetComponent<Animator>().SetBool("IsOpen", false);//Panel
                aquaDiscoveryTrayItems[1].GetComponent<Animator>().SetBool("IsOpen", false);
                StartCoroutine(aqua_delaythenClose());
                break;

        }

       
        
       
    }
    IEnumerator def_delaythenClose()
    {
        yield return new WaitForSeconds(0.2f);
        defaultDiscoveryTrayItems[0].SetActive(false);
        defaultDiscoveryTrayItems[2].SetActive(true);//Opening Button index=2
    }

    IEnumerator forest_delaythenClose()
    {
        yield return new WaitForSeconds(0.2f);
        forestDiscoveryTrayItems[0].SetActive(false);
        forestDiscoveryTrayItems[2].SetActive(true);//Opening Button index=2
    }
    IEnumerator aqua_delaythenClose()
    {
        yield return new WaitForSeconds(0.2f);
        aquaDiscoveryTrayItems[0].SetActive(false);
        aquaDiscoveryTrayItems[2].SetActive(true);//Opening Button index=2
    }

    #endregion


    public void EmptyStringInComManager()
    {
        PlayerPrefs.DeleteKey("parentElement1");
        PlayerPrefs.DeleteKey("parentElement2");

        if (FindObjectOfType<combinationManager>().COM_Element1!=null)
        {
            FindObjectOfType<combinationManager>().COM_Element1 = null;
        }
        if (FindObjectOfType<combinationManager>().COM_Element2 != null)
        {
            FindObjectOfType<combinationManager>().COM_Element2 = null;
        }

    }
}
