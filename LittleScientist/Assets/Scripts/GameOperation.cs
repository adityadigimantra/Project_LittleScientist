using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;

public class GameOperation : MonoBehaviour
{

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
        string key1 = "element1";
        string key2 = "element2";

        PlayerPrefs.DeleteKey(key1);
        PlayerPrefs.DeleteKey(key2);
        PlayerPrefs.SetInt("IsRestart", 1);
        PlayerPrefs.SetInt("GameStarted", 0);
    }
}
