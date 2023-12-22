using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ElementManager : MonoBehaviour
{
    public GameObject[] CopiedElements;
    public GameObject[] NewCreatedElements;

    [Header("Parents and Last GameObjects")]
    public GameObject Parent1Object;
    public GameObject Parent2Object;
    public List<GameObject>ObjectsToSwitchOff=new List<GameObject>();
    public combinationManager comManager;

    [Header("Files")]
    private string saveDisabledGameObjects = "saveDisabledGameobjects.txt";

    private void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
    }
    private void Update()
    {

    }
    
    public void DisablingObjects()
    {
        if(PlayerPrefs.GetInt("elementCreated")==1)
        {
            if (Parent1Object != null && Parent2Object != null)
            {
                if (!comManager.disabledGameobjects.Contains(Parent1Object.name))
                {
                    comManager.disabledGameobjects.Add(Parent1Object.name);
                }
                if (!comManager.disabledGameobjects.Contains(Parent2Object.name))
                {
                    comManager.disabledGameobjects.Add(Parent2Object.name);
                }
                PlayerPrefs.SetInt("elementCreated", 2);
                saveDisabledGameObjectsList();
                Parent1Object.SetActive(false);
                Parent2Object.SetActive(false);

            }

            else
            {
                PlayerPrefs.SetInt("elementCreated", 2);
            }
        }

        if(PlayerPrefs.GetInt("ElementAlreadyPresent")==1)
        {
            StartCoroutine(waitAndPlayAnimation());
            PlayerPrefs.SetInt("ElementAlreadyPresent", 2);
        }
        if(PlayerPrefs.GetInt("NoCombinationFound")==1)
        {
            StartCoroutine(waitAndPlayAnimation());
            PlayerPrefs.SetInt("NoCombinationFound", 2);
        }
    }

    public void MakeGameObjectsNull()
    {
        Parent1Object = null;
        Parent2Object = null;
    }
    
    public void SetParentElements(GameObject obj1,GameObject obj2)
    {
        Parent1Object = obj1;
        Parent2Object = obj2;
        SetLastParentObects(Parent1Object, Parent2Object);
    }

    public void SetLastParentObects(params GameObject[] gameObjects)
    {
        ObjectsToSwitchOff.AddRange(gameObjects);
    }

    public void saveDisabledGameObjectsList()
    {
        string saveDisObj = string.Join(";", FindObjectOfType<combinationManager>().disabledGameobjects.ToArray());
        PlayerPrefs.SetString("DisabledCollidedGameObject", saveDisObj);
        saveDisabledGameobjectListToFile();
        PlayerPrefs.Save();
    }
    public void saveDisabledGameobjectListToFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveDisabledGameObjects);
        try
        {
            using (FileStream fileStream = File.Open(filePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, comManager.disabledGameobjects);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void getDisabledGameobjectsToList()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveDisabledGameObjects);
        try
        {
            using(FileStream fileStream=File.Open(filePath,FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                comManager.disabledGameobjects = (List<string>)binaryFormatter.Deserialize(fileStream);

            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    IEnumerator waitAndPlayAnimation()
    {
        yield return new WaitForSeconds(3f);
        foreach(GameObject g in ObjectsToSwitchOff)
        {
            g.GetComponent<CheckStatus>().thisObjectAnimator.SetBool("IsOpen", false);
        }
    }

}