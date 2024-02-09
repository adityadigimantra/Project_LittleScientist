using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ElementManager : MonoBehaviour
{

    [Header("Instances")]
    public combinationManager comManager;

    [Header("Arrays")]
    public GameObject[] CopiedElements;
    public GameObject[] NewCreatedElements;

    [Header("Parents and Last GameObjects")]
    public GameObject Parent1Object;
    public GameObject Parent2Object;
    public GameObject gameObjectWithCopiedTag;
    public GameObject gameObjectWithNewCreatedElementTag;
    public List<GameObject>ObjectsToSwitchOff=new List<GameObject>();
   

    [Header("Files")]
    private string saveDisabledGameObjects = "saveDisabledGameobjects.txt";
    private string saveTrashGameObjects = "saveTrashGameObjects.txt";
    private string saveScrollRectGameObjects = "saveScrollRectGameObjects.txt";

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

    #region Saving and Creating Disabled GameObject List
    public void saveDisabledGameObjectsList()
    {
        string saveDisObj = string.Join(";", FindObjectOfType<combinationManager>().disabledGameobjects.ToArray());
        //PlayerPrefs.SetString("DisabledCollidedGameObject", saveDisObj);
        saveDisabledGameobjectListToFile();
        //PlayerPrefs.Save();
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
    #endregion

    #region Saving and Creating Trash GameObjects List
    public void SaveTrashGameObjectsList()
    {
        //string savThisList = string.Join(";", FindObjectOfType<combinationManager>().elementsDraggedToTrash.ToArray());
        SaveTrashGameObjectsListToFile();

    }

    public void SaveTrashGameObjectsListToFile()
    {
        string filepath = Path.Combine(Application.persistentDataPath, saveTrashGameObjects);
        try
        {
            using(FileStream fileStream=File.Open(filepath,FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                {
                    binaryFormatter.Serialize(fileStream, comManager.elementsDraggedToTrash);
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void getTrashGameObjectsFileToList()
    {
        string filepath = Path.Combine(Application.persistentDataPath, saveTrashGameObjects);
        try
        {
            using(FileStream fileStream=File.Open(filepath,FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                comManager.elementsDraggedToTrash = (List<string>)binaryFormatter.Deserialize(fileStream);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    #endregion

    #region Saving and Creating Scroll Rects GameObjects File
    public void SaveScrollRectGameObjectsList()
    {
        //string
    }
    #endregion


    #region Combination Related Functions

    public void DisablingElementsCombinationAlreadyPresent()
    {
        Parent1Object.GetComponent<Animator>().SetBool("IsOpen", false);
        Parent2Object.GetComponent<Animator>().SetBool("IsOpen", false);
        HandlesIfMoreThenTwoElementsAreOverlapped();
    }

    public void DisablingElementsWhenNoCombinationFound()
    {
        Parent2Object.transform.GetChild(0).GetComponent<Animator>().SetBool("IsOpen", true);
        Parent1Object.transform.GetChild(0).GetComponent<Animator>().SetBool("IsOpen", true);
        HandlesIfMoreThenTwoElementsAreOverlapped();
    }
    public void HandlesIfMoreThenTwoElementsAreOverlapped()
    {
        foreach(GameObject g in ObjectsToSwitchOff)
        {
            g.transform.GetChild(0).GetComponent<Animator>().SetBool("IsOpen", true);
            //g.transform.GetChild(0).gameObject.SetActive(false);
            g.GetComponent<Element>().isCollided = false;
        }
    }
    #endregion
}