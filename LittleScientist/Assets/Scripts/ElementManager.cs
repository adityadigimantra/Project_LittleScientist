using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    public GameObject[] CopiedElements;
    public GameObject[] NewCreatedElements;

    public GameObject Parent1Object;
    public GameObject Parent2Object;
    public combinationManager comManager;
    private void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
    }
    private void Update()
    {

    }

    public void DisablingObjects()
    {
        if (PlayerPrefs.GetInt("elementCreated") == 1)
        {
            if (Parent1Object != null && Parent2Object != null)
            {
                Destroy(Parent1Object);
                Destroy(Parent2Object);
                PlayerPrefs.SetInt("elementCreated", 0);
                //saveDisabledGameObjectsList();
            }
            else
            {
                //Parent1Object.SetActive(true);
                //Parent2Object.SetActive(true);
                PlayerPrefs.SetInt("elementCreated", 0);
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
        PlayerPrefs.Save();
    }
}