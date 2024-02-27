using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThemeManager : MonoBehaviour
{
  
    [Header("Theme Interface")]

    public Sprite[] SelectedThemeImages;
    public Sprite[] UnSelectedThemeImages;
    public GameObject ThemePanel;
    public GameObject defaultBoardObj;
    public GameObject forestBoardObj;
    public GameObject aquaBoardObj;


    [Header("Theme Interface Objects")]
    public GameObject Def_themeBoard_obj;
    public GameObject[] themeContentObjs;
    public GameObject[] themeCloseButtons;

    public string selectedTheme;
    public GameObject SettingMenu;

    public GameObject themeChangingLoadingPanel;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello From Theme Manager");

       switch (selectedTheme)
       {
           case "Default":
               LoadDefaultTheme();
               break;

           case "Forest":
               LoadForestTheme();
               break;

           case "Aqua":
               LoadAquaTheme();
               break;

       }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SettingIfNoThemeSelected()
    {
        if(string.IsNullOrEmpty(PlayerPrefs.GetString("Theme")))
        {
            PlayerPrefs.SetString("Theme", "Default");
        }
    }
    public void SelectDefaultTheme()
    {
        
        PlayerPrefs.SetString("Theme", "Default");
        themeContentObjs[0].GetComponent<Image>().sprite = SelectedThemeImages[0];
        themeChangingLoadingPanel.SetActive(true);
        //defaultBoardObj.SetActive(true);
        //themeCloseButtons[0].SetActive(true);
        UnSelectForestTheme();
        UnSelectAquaTheme();
        PlayerPrefs.SetInt("IsRestart", 1);
        if (selectedTheme!="Default")
        {
            StartCoroutine(ChangeScene());
        }

    }

    public void LoadDefaultTheme()
    {
        themeChangingLoadingPanel.SetActive(false);
        themeContentObjs[0].GetComponent<Image>().sprite = SelectedThemeImages[0];
        defaultBoardObj.SetActive(true);
        themeCloseButtons[0].SetActive(true);
    }

    public void UnselectDefaultTheme()
    {
        themeContentObjs[0].GetComponent<Image>().sprite = UnSelectedThemeImages[0];
        defaultBoardObj.SetActive(false);
        themeCloseButtons[0].SetActive(false);
    }

    public void SelectForestTheme()
    {
        PlayerPrefs.SetString("Theme", "Forest");
        themeContentObjs[1].GetComponent<Image>().sprite = SelectedThemeImages[1];
        themeChangingLoadingPanel.SetActive(true);
        //forestBoardObj.SetActive(true);
        //themeCloseButtons[1].SetActive(true);
        UnselectDefaultTheme();
        UnSelectAquaTheme();
        PlayerPrefs.SetInt("IsRestart", 1);
        if (selectedTheme!="Forest")
        {
            StartCoroutine(ChangeScene());
        }

    }

    public void LoadForestTheme()
    {
        themeChangingLoadingPanel.SetActive(false);
        themeContentObjs[1].GetComponent<Image>().sprite = SelectedThemeImages[1];
        forestBoardObj.SetActive(true);
        themeCloseButtons[1].SetActive(true);
    }

    public void UnSelectForestTheme()
    {
        themeContentObjs[1].GetComponent<Image>().sprite = UnSelectedThemeImages[1];
        //forestBoardObj.SetActive(false);
        //themeCloseButtons[1].SetActive(false);
    }

    public void SelectAquaTheme()
    {
        PlayerPrefs.SetString("Theme", "Aqua");
        themeContentObjs[2].GetComponent<Image>().sprite = SelectedThemeImages[2];
        themeChangingLoadingPanel.SetActive(true);
        //aquaBoardObj.SetActive(true);
        //themeCloseButtons[2].SetActive(true);
        UnselectDefaultTheme();
        UnSelectForestTheme();
        PlayerPrefs.SetInt("IsRestart", 1);
        if (selectedTheme != "Aqua")
        {
            StartCoroutine(ChangeScene());
        }
    }

    public void LoadAquaTheme()
    {
        themeChangingLoadingPanel.SetActive(false);
        themeContentObjs[2].GetComponent<Image>().sprite = SelectedThemeImages[2];
        aquaBoardObj.SetActive(true);
        themeCloseButtons[2].SetActive(true);
    }
    public void UnSelectAquaTheme()
    {
        themeContentObjs[2].GetComponent<Image>().sprite = UnSelectedThemeImages[2];
        //aquaBoardObj.SetActive(false);
        //themeCloseButtons[2].SetActive(false);
    }

    public void OpenThemePanel()
    {
        ThemePanel.SetActive(true);
        SettingMenu.SetActive(false);
    }
    public void CloseThemePanel()
    {
        ThemePanel.SetActive(false);
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
