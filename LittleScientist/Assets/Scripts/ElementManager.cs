using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    public GameObject[] CopiedElements;
    public GameObject[] NewCreatedElements;

    private void Update()
    {
        CopiedElements= GameObject.FindGameObjectsWithTag("Copied"); 
        NewCreatedElements= GameObject.FindGameObjectsWithTag("NewCreatedElement");
        DisablingObjects();
    }

    public void DisablingObjects()
    {
        if (PlayerPrefs.GetInt("elementCreated") == 1)
        {
            //To Search all the Elements with same Name Present in the Scene
            foreach (GameObject g in CopiedElements)
            {
                if (g.GetComponent<Element>().isCollided==true)
                {
                    g.SetActive(false);
                    FindObjectOfType<combinationManager>().disabledGameobjects.Add(g.name);
                }
            }
            foreach (GameObject g in NewCreatedElements)
            {
                if (g.GetComponent<Element>().isCollided==true)
                {
                    g.SetActive(false);
                    FindObjectOfType<combinationManager>().disabledGameobjects.Add(g.name);
                }
            }
            saveDisabledGameObjectsList();
        }
    }

    public void saveDisabledGameObjectsList()
    {
        string saveDisObj = string.Join(";", FindObjectOfType<combinationManager>().disabledGameobjects.ToArray());
        PlayerPrefs.SetString("DisabledCollidedGameObject", saveDisObj);
        PlayerPrefs.SetInt("elementCreated", 0);
        PlayerPrefs.Save();
    }
}
