using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject searchPanel;
    public Sprite goImage;
    public Sprite closeImage;

    [Header("GameObject")]
    public string searchedString;
    public GameObject foundObject;
    public GameObject goButton;

    [Header("Scroll Elements")]
    public InputField searchInputField;
    public ScrollRect bottomScrollRect;
    public Scrollbar bottomScrollbar;

    private void Start()
    {
        searchInputField.onEndEdit.AddListener(OnEndEdit);
    }
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

    void OnEndEdit(string objName)
    {
        if(Input.GetKey(KeyCode.Return))
        {
            StartSearching();
        }
    }

    public GameObject searchGameObject(string objName)
    {
        foreach (Transform child in bottomScrollRect.content.transform)
        {
            if (child.name.ToLower().Contains(objName.ToLower()))
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
        searchInputField.text = "";
    }
    private void Update()
    {
        if(string.IsNullOrEmpty(searchInputField.text))
        {
            goButton.GetComponent<Image>().sprite = closeImage;
            Debug.Log("Running");
        }
        else
        {
            goButton.GetComponent<Image>().sprite = goImage;
            Debug.Log("Running2");
        }
    }

}
