using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combinationManager : MonoBehaviour
{
    Combination resultCombination;
    Combination resultCombination2;
    public ElementLoader elementLoaderObj;
    public Element elementObj;
    public string COM_Element1;
    public string COM_Element2;
    public bool creatingNewElement=true;

    [Header("Lists")]
    public List<string> CreatedElements = new List<string>();
    public List<string> loadCreatedElements = new List<string>();

    private void Start()
    {
        elementLoaderObj = FindObjectOfType<ElementLoader>();
        elementObj = FindObjectOfType<Element>();
        LoadCreatedElementList();

    }

    private void Update()
    {
        //Elements Name coming from collision
        COM_Element1=elementObj.ELE_Element1;
        COM_Element2=elementObj.ELE_Element2;

        resultCombination=FindCombination1(COM_Element1,COM_Element2);
        if (resultCombination != null)
        {
            Debug.Log("Result:" + resultCombination.result);
            if(creatingNewElement)
            {
                createNewElement();
            }
            
        }
        else
        {
            Debug.Log("Result:No Combinations Found");
        }

            
    }

    public void createNewElement()
    {
        
        if(!CreatedElements.Contains(resultCombination.result))
        {
            CreatedElements.Add(resultCombination.result);
            for(int i=0;i<CreatedElements.Count;i++)
            {
                PlayerPrefs.SetString("CreatedElementData" + i, CreatedElements[i]);
            }
            PlayerPrefs.SetInt("StringCount", CreatedElements.Count);
        }
        creatingNewElement = false;
    }

    public void LoadCreatedElementList()
    {
        int count = PlayerPrefs.GetInt("StringCount", 0);
        for(int i=0;i<count;i++)
        {
            string loadedString = PlayerPrefs.GetString("CreatedElementData" + i);
            loadCreatedElements.Add(loadedString);
        }
    }

    private Combination FindCombination1(string el1,string el2)
    {
        foreach(Combination comb in elementLoaderObj.elementsList)
        {
            if(comb.element1==el1 && comb.element2==el2)
            {
                return comb;
            }
        }
        return null;
    }

//***************************************************************************************************************
    //Goes in Update
    /*
resultCombination2 = FindCombination2(COM_Element2, COM_Element1);
if(resultCombination2!=null)
{
    Debug.Log("Result 2:" + resultCombination2.result);
}
else
{
    Debug.Log("Result:No Combination Found");
}
  */

    //New Method
    /*
private Combination FindCombination2(string el2, string el1)
{
    foreach (Combination comb in elementLoaderObj.elementsList)
    {
        if (comb.element2 == el2 && comb.element1 == el1)
        {
            return comb;
        }
    }
    return null;
}
*/
}
