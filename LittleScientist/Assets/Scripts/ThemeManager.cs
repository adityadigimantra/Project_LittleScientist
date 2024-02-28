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
    public GameObject ParentThemePanel;
    public GameObject defaultBoardObj;
    public GameObject forestBoardObj;
    public GameObject aquaBoardObj;


    [Header("Theme Interface Objects")]
    public GameObject Def_themeBoard_obj;
    public GameObject[] themeContentObjs;
    public GameObject[] themeCloseButtons;


    [Header("SettinsMenus")]
    public GameObject parentSettingMenu;
    public GameObject def_settingsMenu;
    public GameObject def_closeButton;
    public GameObject for_settingsMenu;
    public GameObject for_closeButton;
    public GameObject aqua_settingsMenu;
    public GameObject aqua_closeButton;

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
        selectedTheme = PlayerPrefs.GetString("Theme");
        switch (selectedTheme)
        {
            case "Default":
                themeContentObjs[0].GetComponent<Image>().sprite = SelectedThemeImages[0];
                break;

            case "Forest":
                themeContentObjs[1].GetComponent<Image>().sprite = SelectedThemeImages[1];
                break;

            case "Aqua":
                themeContentObjs[2].GetComponent<Image>().sprite = SelectedThemeImages[2];
                break;

        }
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
        UnSelectForestTheme();
        UnSelectAquaTheme();
       
        if (selectedTheme!="Default")
        {
            themeChangingLoadingPanel.SetActive(true);
            PlayerPrefs.SetInt("IsRestart", 1);
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
        //forestBoardObj.SetActive(true);
        //themeCloseButtons[1].SetActive(true);
        UnselectDefaultTheme();
        UnSelectAquaTheme();

        if (selectedTheme!="Forest")
        {
            themeChangingLoadingPanel.SetActive(true);
            PlayerPrefs.SetInt("IsRestart", 1);
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
        UnselectDefaultTheme();
        UnSelectForestTheme();
       
        if (selectedTheme != "Aqua")
        {
            themeChangingLoadingPanel.SetActive(true);
            PlayerPrefs.SetInt("IsRestart", 1);
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

        ParentThemePanel.SetActive(true);
        switch (selectedTheme)
        {
            case "Default":
                defaultBoardObj.SetActive(true);
                def_closeButton.SetActive(true);
                def_settingsMenu.SetActive(false);
                break;

            case "Forest":
                forestBoardObj.SetActive(true);
                for_closeButton.SetActive(true);
                for_settingsMenu.SetActive(false);
                break;

            case "Aqua":
                aquaBoardObj.SetActive(true);
                aqua_closeButton.SetActive(true);
                aqua_settingsMenu.SetActive(false);
                break;
        }

       
    }
    public void CloseThemePanel()
    {
        ParentThemePanel.SetActive(false);
        parentSettingMenu.SetActive(false);
        switch (selectedTheme)
        {
            case "Default":
                defaultBoardObj.SetActive(false);
                def_closeButton.SetActive(false);
               
                break;

            case "Forest":
                forestBoardObj.SetActive(false);
                for_closeButton.SetActive(false);
                break;

            case "Aqua":
                aquaBoardObj.SetActive(false);
                aqua_closeButton.SetActive(false);
                break;
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
