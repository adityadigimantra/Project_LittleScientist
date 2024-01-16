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
        StartCoroutine(goToScene1());
    }


    IEnumerator goToScene1()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneNumbertoLoad);
    }
}
