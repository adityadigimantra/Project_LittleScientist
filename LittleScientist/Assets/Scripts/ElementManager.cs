using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    public GameObject[] CopiedElements;
    public GameObject[] NewCreatedElements;

    public GameObject Parent1Object;
    public GameObject Parent2Object;
    private void Update()
    {

    }

    public void DisablingObjects()
    {
        if (PlayerPrefs.GetInt("elementCreated") == 1)
        {
            if (Parent1Object != null && Parent2Object != null)
            {
                Parent1Object.SetActive(false);
                Parent2Object.SetActive(false);
                FindObjectOfType<combinationManager>().disabledGameobjects.Add(Parent1Object.name);
                FindObjectOfType<combinationManager>().disabledGameobjects.Add(Parent2Object.name);
                saveDisabledGameObjectsList();
            }
        }

    }

    public void SetParentElements(GameObject obj1,GameObject obj2)
    {
        Parent1Object = obj1;
        Parent2Object = obj2;
    }

    public void saveDisabledGameObjectsList()
    {
        string saveDisObj = string.Join(";", FindObjectOfType<combinationManager>().disabledGameobjects.ToArray());
        PlayerPrefs.SetString("DisabledCollidedGameObject", saveDisObj);
        PlayerPrefs.SetInt("elementCreated", 0);
        PlayerPrefs.Save();
    }
}