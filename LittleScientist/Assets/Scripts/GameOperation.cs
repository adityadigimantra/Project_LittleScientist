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
    public GameObject TopElementView;
    public int TopElementViewChildCount;
    public GameObject[] UIControls;
    public GameObject discorveryTrayContent;
    public int discorveryTrayContentChildCount;
    public GameObject CreateSomeElementText;
    public GameObject SettingsPanel;

    [Header("Discovery Trays")]
    public GameObject[] defaultDiscoveryTrayItems;
    public GameObject[] forestDiscoveryTrayItems;
    public GameObject[] aquaDiscoveryTrayItems;

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



    }

    public void ThemeApplyer()
    {
        selectedTheme = PlayerPrefs.GetString("Theme");
        switch (selectedTheme)
        {
            case "Default":
                foreach (GameObject g in defaultThemeGameItems)
                {
                    g.SetActive(true);
                }
                foreach(GameObject g in forestThemeGameItems)
                {
                    g.SetActive(false);
                }
                foreach(GameObject g in AquaThemeGameItems)
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

    private void Update()
    {
        ThemeApplyer();
        TopElementViewChildCount = TopElementView.transform.childCount;
        if(TopElementViewChildCount>=8)
        {
            SwitchOnControls();
        }
        discorveryTrayContentChildCount = discorveryTrayContent.transform.childCount;
        if(discorveryTrayContentChildCount>=1)
        {
            CreateSomeElementText.SetActive(false);
        }
        else
        {
            CreateSomeElementText.SetActive(true);
        }

    }
    public void SwitchOnControls()
    {
        foreach(GameObject g in UIControls)
        {
            g.SetActive(true);
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
