using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElementLoader : MonoBehaviour
{
    public CombinationData combinationData;
    public TextAsset jsonFile;
    public List<Combination> elementsList;
    Combination combination;
    Combination resultCombination;
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

        string element1 = "air";
        string element2 = "fire";
        resultCombination=findCombination(element1,element2);
        if(resultCombination!=null)
        {
            Debug.Log("Combination Found:" + resultCombination.result);
        }
        else
        {
            Debug.Log("Combinations not found");
        }

        
        
        /*
        bool elementExists = elementsList.Any(combination => combination.element1 == "earth" && combination.element2=="water");
        if(elementExists)
        {
            Debug.Log("Mud formed");
        }
        else
        {
            Debug.Log("no file found");
        }
        */
    }

    private Combination findCombination(string el1,string el2)
    {
        foreach(Combination combination in elementsList)
        {
            if(combination.element1==el1 && combination.element2==el2)
            {
                return combination;
                
            }
        }
        return null;
    }

    
}








