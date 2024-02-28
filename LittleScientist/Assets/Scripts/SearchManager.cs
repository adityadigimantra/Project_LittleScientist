using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchManager : MonoBehaviour
{
    [Header("Def Theme Search Panels")]
    public GameObject def_searchPanel;
    public GameObject def_goButton;
    public InputField def_searchInputField;
    public ScrollRect def_bottomScrollRect;
    public Scrollbar def_bottomScrollbar;

    [Header("Def Theme Search Panels")]
    public GameObject for_searchPanel;
    public GameObject for_goButton;
    public InputField for_searchInputField;
    public ScrollRect for_bottomScrollRect;
    public Scrollbar for_bottomScrollbar;

    [Header("Def Theme Search Panels")]
    public GameObject aqua_searchPanel;
    public GameObject aqua_goButton;
    public InputField aqua_searchInputField;
    public ScrollRect aqua_bottomScrollRect;
    public Scrollbar aqua_bottomScrollbar;

    [Header("GameObject")]
    public string searchedString;
    public GameObject foundObject;

    public Sprite goImage;
    public Sprite closeImage;

    public string selectedTheme;
   
    private void Start()
    {
        selectedTheme = PlayerPrefs.GetString("Theme");
        switch (selectedTheme)
        {
            case "Default":
                def_searchInputField.onEndEdit.AddListener(OnEndEdit);
                break;

            case "Forest":
                for_searchInputField.onEndEdit.AddListener(OnEndEdit);
                break;

            case "Aqua":
                aqua_searchInputField.onEndEdit.AddListener(OnEndEdit);
                break;

        }


    }
    public void StartSearching()
    {
        switch(selectedTheme)
        {
            case "Default":
                searchedString = ConvertToLowerCase(def_searchInputField.text);
                foundObject = searchGameObject(searchedString);
                if (foundObject != null)
                {
                    MoveGameObjectToTop(foundObject);
                }
                def_searchPanel.GetComponent<Animator>().SetBool("IsOpen", false);
                break;

            case "Forest":
                searchedString = ConvertToLowerCase(for_searchInputField.text);
                foundObject = searchGameObject(searchedString);
                if (foundObject != null)
                {
                    MoveGameObjectToTop(foundObject);
                }
                for_searchPanel.GetComponent<Animator>().SetBool("IsOpen", false);
                break;

            case "Aqua":
                searchedString = ConvertToLowerCase(aqua_searchInputField.text);
                foundObject = searchGameObject(searchedString);
                if (foundObject != null)
                {
                    MoveGameObjectToTop(foundObject);
                }
                aqua_searchPanel.GetComponent<Animator>().SetBool("IsOpen", false);
                break;
        }
       
    }

    void OnEndEdit(string objName)
    {
        if(Input.GetKey(KeyCode.Return))
        {
            StartSearching();
        }
    }
    public string ConvertToLowerCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;

        }
        else
        {
            return char.ToLower(input[0]) + input.Substring(1);
        }
    }

    public GameObject searchGameObject(string objName)
    {
        switch(selectedTheme)
        {
            case "Default":
                foreach (Transform child in def_bottomScrollRect.content.transform)
                {
                    if (child.name.ToLower().Contains(objName.ToLower()))
                    {
                        return child.gameObject;
                    }
                }
       
                break;

            case "Forest":
                foreach (Transform child in for_bottomScrollRect.content.transform)
                {
                    if (child.name.ToLower().Contains(objName.ToLower()))
                    {
                        return child.gameObject;
                    }
                }
                break;

            case "Aqua":
                foreach (Transform child in aqua_bottomScrollRect.content.transform)
                {
                    if (child.name.ToLower().Contains(objName.ToLower()))
                    {
                        return child.gameObject;
                    }
                }
                break;
        }
        return null;
    }

    public void MoveGameObjectToTop(GameObject obj)
    {
        switch(selectedTheme)
        {
            case "Default":
                obj.transform.SetAsFirstSibling();
                def_bottomScrollbar.GetComponent<Scrollbar>().value = 0;
                break;

            case "Forest":
                obj.transform.SetAsFirstSibling();
                for_bottomScrollbar.GetComponent<Scrollbar>().value = 0;
                break;

            case "Aqua":
                obj.transform.SetAsFirstSibling();
                aqua_bottomScrollbar.GetComponent<Scrollbar>().value = 0;
                break;
        }
        
    }

    public void OpenSearchTray()
    {
        switch(selectedTheme)
        {
            case "Default":
                foundObject = null;
                def_searchPanel.SetActive(true);
                def_searchPanel.GetComponent<Animator>().SetBool("IsOpen", true);
                def_searchInputField.text = "";
                for_searchPanel.SetActive(false);
                aqua_searchPanel.SetActive(false);
                break;

            case "Forest":
                foundObject = null;
                for_searchPanel.SetActive(true);
                for_searchPanel.GetComponent<Animator>().SetBool("IsOpen", true);
                for_searchInputField.text = "";
                def_searchPanel.SetActive(false);
                aqua_searchPanel.SetActive(false);
                break;

            case "Aqua":
                foundObject = null;
                aqua_searchPanel.SetActive(true);
                aqua_searchPanel.GetComponent<Animator>().SetBool("IsOpen", true);
                aqua_searchInputField.text = "";
                for_searchPanel.SetActive(false);
                def_searchPanel.SetActive(false);
                break;
        }

        
    }
    private void Update()
    {
        switch (selectedTheme)
        {
            case "Default":
                if (string.IsNullOrEmpty(def_searchInputField.text))
                {
                    def_goButton.GetComponent<Image>().sprite = closeImage;

                }
                else
                {
                    def_goButton.GetComponent<Image>().sprite = goImage;

                }
                break;

            case "Forest":
                if (string.IsNullOrEmpty(for_searchInputField.text))
                {
                    for_goButton.GetComponent<Image>().sprite = closeImage;

                }
                else
                {
                    for_goButton.GetComponent<Image>().sprite = goImage;

                }
                break;

            case "Aqua":
                if (string.IsNullOrEmpty(aqua_searchInputField.text))
                {
                    aqua_goButton.GetComponent<Image>().sprite = closeImage;

                }
                else
                {
                    aqua_goButton.GetComponent<Image>().sprite = goImage;

                }
                break;
        }

        
    }

}
