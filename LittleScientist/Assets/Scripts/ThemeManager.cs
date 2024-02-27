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
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        selectedTheme = PlayerPrefs.GetString("Theme");
        if (string.IsNullOrEmpty(selectedTheme))
        {
            SelectDefaultTheme();
        }
        else
        {
            switch (selectedTheme)
            {
                case "Default":
                    SelectDefaultTheme();
                    break;

                case "Forest":
                    SelectForestTheme();
                    break;

                case "Aqua":
                    SelectAquaTheme();
                    break;

            }

        }
    }

    public void SelectDefaultTheme()
    {
        
        PlayerPrefs.SetString("Theme", "Default");
        themeContentObjs[0].GetComponent<Image>().sprite = SelectedThemeImages[0];
        defaultBoardObj.SetActive(true);
        themeCloseButtons[0].SetActive(true);
        UnSelectForestTheme();
        UnSelectAquaTheme();
        if(selectedTheme!="Default")
        {
            SceneManager.LoadScene(1);
        }

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
        forestBoardObj.SetActive(true);
        themeCloseButtons[1].SetActive(true);
        UnselectDefaultTheme();
        UnSelectAquaTheme();

        if(selectedTheme!="Forest")
        {
            SceneManager.LoadScene(1);
        }

    }

    public void UnSelectForestTheme()
    {
        themeContentObjs[1].GetComponent<Image>().sprite = UnSelectedThemeImages[1];
        forestBoardObj.SetActive(false);
        themeCloseButtons[1].SetActive(false);
    }

    public void SelectAquaTheme()
    {
        PlayerPrefs.SetString("Theme", "Aqua");
        themeContentObjs[2].GetComponent<Image>().sprite = SelectedThemeImages[2];
        aquaBoardObj.SetActive(true);
        themeCloseButtons[2].SetActive(true);
        UnselectDefaultTheme();
        UnSelectForestTheme();

        if (selectedTheme != "Aqua")
        {
            SceneManager.LoadScene(1);
        }
    }
    public void UnSelectAquaTheme()
    {
        themeContentObjs[2].GetComponent<Image>().sprite = UnSelectedThemeImages[2];
        aquaBoardObj.SetActive(false);
        themeCloseButtons[2].SetActive(false);
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
}
