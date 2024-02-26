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
    public GameObject discoveryTrayPanel;
    public GameObject discoveryTrayObject;
    public GameObject OpenDiscoveryTrayButton;
    public GameObject lowerSmallPanel;
    public GameObject TopElementView;
    public int TopElementViewChildCount;
    public GameObject[] UIControls;
    public GameObject discorveryTrayContent;
    public int discorveryTrayContentChildCount;
    public GameObject CreateSomeElementText;
    public GameObject SettingsPanel;

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

    private void Update()
    {
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

    public void OpenDiscoveryTray()
    {
        discoveryTrayPanel.SetActive(true);
        discoveryTrayPanel.GetComponent<Animator>().SetBool("IsOpen", true);
        discoveryTrayObject.GetComponent<Animator>().SetBool("IsOpen", true);
        OpenDiscoveryTrayButton.SetActive(false);

    }
    public void CloseDiscoveryPanel()
    {
        discoveryTrayObject.GetComponent<Animator>().SetBool("IsOpen", false);
        discoveryTrayPanel.GetComponent<Animator>().SetBool("IsOpen", false);
        StartCoroutine(delaythenClose());
    }
    IEnumerator delaythenClose()
    {
        yield return new WaitForSeconds(0.2f);
        discoveryTrayPanel.SetActive(false);
        OpenDiscoveryTrayButton.SetActive(true);
    }

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
