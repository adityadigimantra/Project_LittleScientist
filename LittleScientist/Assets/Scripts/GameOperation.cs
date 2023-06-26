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

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
        string key1 = "element1";
        string key2 = "element2";

        PlayerPrefs.DeleteKey(key1);
        PlayerPrefs.DeleteKey(key2);
        PlayerPrefs.SetInt("IsRestart", 1);
        GameState = GameState.Prepare;
        Debug.Log("GameState is "+GameState);
    }
}
