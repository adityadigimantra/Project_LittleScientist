using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ElementPosition : MonoBehaviour
{
    public Vector2 InitialPos;
    public Vector3 FinalPos;
    public Vector2 newFinalPos;
    
    

    [Header("Files")]
    private string saveFinalElementPositions = "saveElementFinalPosition.txt";


    private void Start()
    {
        GetPositonFromList();
    }

    private void Update()
    {
      
    }

    public void GetInitialPos(Vector2 initialPos)
    {
        InitialPos = initialPos;
    }
    public void GetFinalPos(Vector2 finalPos)
    {
        FinalPos = finalPos;
        SetPositionToList();
    }
    public void SetPositionToList()
    {
        string convertedPosString = FindObjectOfType<combinationManager>().ConvertVectorToString(FinalPos);
        PlayerPrefs.SetString(gameObject.name, convertedPosString);
        if (!FindObjectOfType<combinationManager>().FinalSavedPosition.Contains(gameObject.name+":"+convertedPosString))
        {
            FindObjectOfType<combinationManager>().FinalSavedPosition.Add(gameObject.name+":"+convertedPosString);
            SetListToFile();
        }
    }
    public void SetListToFile()
    {
        string FilePath = Path.Combine(Application.persistentDataPath, saveFinalElementPositions);
        try
        {
            using (FileStream fileStream = File.Open(FilePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, FindObjectOfType<combinationManager>().FinalSavedPosition);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void GetListFromFile()
    {
        string FilePath = Path.Combine(Application.persistentDataPath, saveFinalElementPositions);
        try
        {
            using (FileStream fileStream = File.Open(FilePath, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FindObjectOfType<combinationManager>().FinalSavedPosition = (List<string>)binaryFormatter.Deserialize(fileStream);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void GetPositonFromList()
    {
        GetListFromFile();
        if (FindObjectOfType<combinationManager>().FinalSavedPosition.Count > 0)
        {
            foreach (string str in FindObjectOfType<combinationManager>().FinalSavedPosition)
            {
                string[] parts = str.Split(':');
                if (parts.Length >= 2 && parts[0] == gameObject.name)
                {
                    newFinalPos = FindObjectOfType<combinationManager>().convertStringToVector(parts[1]);
                    gameObject.transform.position = newFinalPos;

                }
            }

        }
    }
}
