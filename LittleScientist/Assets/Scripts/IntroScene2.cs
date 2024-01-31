using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene2 : MonoBehaviour
{
    // Start is called before the first frame updat
    public GameObject onboardingScreen;
    private void Start()
    {
        if(PlayerPrefs.GetInt("OnboardingDone")==0)
        {
            StartCoroutine(StartOnboardingPlayer());
        }
        else
        {
            StartCoroutine(ChangeScene());
        }


    }
    IEnumerator StartOnboardingPlayer()
    {
        yield return new WaitForSeconds(3f);
        onboardingScreen.SetActive(true);
        onboardingScreen.GetComponent<Animator>().SetBool("IsOpen", true);
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(2);
    }
}
