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
    public List<string> FinalSavedPosition = new List<string>();
    

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
        if (!FinalSavedPosition.Contains(gameObject.name+":"+convertedPosString))
        {
            FinalSavedPosition.Add(gameObject.name+":"+convertedPosString);
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
                binaryFormatter.Serialize(fileStream, FinalSavedPosition);
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
                FinalSavedPosition = (List<string>)binaryFormatter.Deserialize(fileStream);
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
        if(FinalSavedPosition.Count>0)
        {
            string PosInString = FinalSavedPosition[FinalSavedPosition.Count - 1];
            newFinalPos = FindObjectOfType<combinationManager>().convertStringToVector(PosInString);
            gameObject.transform.position = newFinalPos;
        }
        
    }
}
