using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoBehaviour
{
    [Header("Default Theme Data")]
    public Image[] Def_mainBoardImages;
    public Image[] Def_LowerIconsImages;
    public Image[] Def_DiscoveryTrayImages;

    [Header("Forest Theme Data")]
    public Image[] for_mainBoardImages;
    public Image[] for_LowerIconsImages;
    public Image[] for_DiscoveryTrayImages;

    [Header("Aqua Theme Data")]
    public Image[] Aqu_mainBoardImages;
    public Image[] Aqu_LowerIconsImages;
    public Image[] Aqu_DiscoveryTrayImages;

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

    [Header("Default Theme Objects")]
    public Image def_mainBoard_obj;
    public Image def_innerBoard_obj;
    public Image def_trash_obj;
    public Image def_trashLine_obj;

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
    }

    public void SelectDefaultTheme()
    {
        PlayerPrefs.SetString("Theme", "default");
        themeContentObjs[0].GetComponent<Image>().sprite = SelectedThemeImages[0];
        defaultBoardObj.SetActive(true);
        themeCloseButtons[0].SetActive(true);
        UnSelectForestTheme();
        UnSelectAquaTheme();

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
