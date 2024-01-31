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
        StartCoroutine(ChangeScene());
    }
    IEnumerator ChangeScene()
    {
       if(PlayerPrefs.GetInt("OnboardingDone")==0)
        {
            yield return new WaitForSeconds(2f);
            onboardingScreen.SetActive(true);
            //onboardingScreen.GetComponent<Animator>().SetBool("IsOpen", true);
        }
       else
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(2);
        }
        
    }
}
