/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class YourScriptName : MonoBehaviour
{
    private List<string> CreatedElements = new List<string>();
    private string saveFilePath = "CreatedElementsData.dat";

    private void Start()
    {
        // ... Your existing initialization code ...

        LoadSavedCreatedElements();
        LoadDisabledGameObjectsList();
    }

    // ... Other functions ...

    public void createNewElement()
    {
        if (!CreatedElements.Contains(resultCombination.result))
        {
            CreatedElements.Add(resultCombination.result);
            SaveCreatedElementsToFile();
            
            // ... Rest of the code ...
        }
    }

    public void SaveCreatedElementsToFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFilePath);

        try
        {
            using (FileStream fileStream = File.Open(filePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, CreatedElements);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error while saving created elements: " + e.Message);
        }
    }

    public void LoadSavedCreatedElements()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFilePath);

        if (File.Exists(filePath))
        {
            try
            {
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    CreatedElements = (List<string>)binaryFormatter.Deserialize(fileStream);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error while loading created elements: " + e.Message);
            }
        }
    }

    // ... Other functions ...
}
*/