using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene1 : MonoBehaviour
{
    public int SceneNumbertoLoad;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startSplashScreen());
    }

    IEnumerator startSplashScreen()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneNumbertoLoad);

    }
}
