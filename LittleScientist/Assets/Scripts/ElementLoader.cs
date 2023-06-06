using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementLoader : MonoBehaviour
{
    public CombinationData combinationData;
    public TextAsset jsonFile;

    // Loading Json File
    private void Start()
    {
        if (jsonFile != null)
        {
            string json = jsonFile.text;
            combinationData = JsonUtility.FromJson<CombinationData>(json);
            Debug.Log(json);
        }
        else
        {
            Debug.Log("File Not Found");
        }
    }
}








