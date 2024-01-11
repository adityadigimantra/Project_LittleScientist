using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject searchPanel;

    [Header("GameObject")]
    public string searchedString;
    public GameObject foundObject;

    [Header("Scroll Elements")]
    public InputField searchInputField;
    public ScrollRect bottomScrollRect;
    public Scrollbar bottomScrollbar;
    public void StartSearching()
    {
        searchedString = searchInputField.text;
        foundObject = searchGameObject(searchedString);
        if(foundObject!=null)
        {
            MoveGameObjectToTop(foundObject);
        }
        searchPanel.GetComponent<Animator>().SetBool("IsOpen", false);
    }

    public GameObject searchGameObject(string objName)
    {
        foreach (Transform child in bottomScrollRect.content.transform)
        {
            if (child.name.ToLower().Contains(searchedString.ToLower()))
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public void MoveGameObjectToTop(GameObject obj)
    {
        obj.transform.SetAsFirstSibling();
        bottomScrollbar.GetComponent<Scrollbar>().value = 0;
        
    }

    public void OpenSearchTray()
    {
        foundObject = null;
        searchPanel.SetActive(true);
        searchPanel.GetComponent<Animator>().SetBool("IsOpen", true);
    }


}
