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
    public Image[] ThemePanelImages;


    [Header("Theme Interface Objects")]
    public GameObject Def_themeBoard_obj;
    public GameObject def_contentObj;
    public GameObject For_contentObj;
    public GameObject Aqu_contentObj;

    [Header("Default Theme Objects")]
    public Image def_mainBoard_obj;
    public Image def_innerBoard_obj;
    public Image def_trash_obj;
    public Image def_trashLine_obj;

    public string selectedTheme;
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
        def_contentObj.GetComponent<Image>().sprite = SelectedThemeImages[0];
        UnSelectForestTheme();
        UnSelectAquaTheme();

    }

    public void UnselectDefaultTheme()
    {
        def_contentObj.GetComponent<Image>().sprite = UnSelectedThemeImages[0];
    }

    public void SelectForestTheme()
    {
        PlayerPrefs.SetString("Theme", "Forest");
        For_contentObj.GetComponent<Image>().sprite = SelectedThemeImages[1];
        UnselectDefaultTheme();
        UnSelectAquaTheme();

    }

    public void UnSelectForestTheme()
    {
        For_contentObj.GetComponent<Image>().sprite = UnSelectedThemeImages[1];
    }

    public void SelectAquaTheme()
    {
        PlayerPrefs.SetString("Theme", "Aqua");
        Aqu_contentObj.GetComponent<Image>().sprite = SelectedThemeImages[2];
        UnselectDefaultTheme();
        UnSelectForestTheme();
    }
    public void UnSelectAquaTheme()
    {
        Aqu_contentObj.GetComponent<Image>().sprite = UnSelectedThemeImages[2];
    }







    public void OpenThemePanel()
    {
        ThemePanel.SetActive(true);
    }
    public void CloseThemePanel()
    {
        ThemePanel.SetActive(false);
    }
}
