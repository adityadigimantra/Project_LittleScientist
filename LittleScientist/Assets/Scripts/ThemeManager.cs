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
    public Image[] SelectedThemeImages;
    public Image[] UnSelectedThemeImages;
    public GameObject ThemePanel;
    public Image[] ThemePanelImages;


    [Header("Theme Interface Objects")]
    public GameObject Def_themeBoard_obj;

    [Header("Default Theme Objects")]
    public Image def_mainBoard_obj;
    public Image def_innerBoard_obj;
    public Image def_trash_obj;
    public Image def_trashLine_obj;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
