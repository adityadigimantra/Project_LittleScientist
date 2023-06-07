using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElementLoader : MonoBehaviour
{
    public CombinationData combinationData;
    public TextAsset jsonFile;
    public List<Combination> elementsList;
    // Loading Json File
    private void Start()
    {
        if (jsonFile != null)
        {
            string json = jsonFile.text;
            combinationData = JsonUtility.FromJson<CombinationData>(json);
            elementsList = combinationData.combinations;
            Debug.Log(json);
        }
        else
        {
            Debug.Log("File Not Found");
        }
    }
}








