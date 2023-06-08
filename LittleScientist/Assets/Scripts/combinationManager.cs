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

    private void Start()
    {
        elementLoaderObj = FindObjectOfType<ElementLoader>();
        elementObj = FindObjectOfType<Element>();

    }

    private void Update()
    {
        //Elements Name coming from collision
        COM_Element1=elementObj.ELE_Element1;
        COM_Element2=elementObj.ELE_Element2;

        resultCombination=FindCombination1(COM_Element1,COM_Element2);
        if(resultCombination!=null)
        {
            Debug.Log("Result:" + resultCombination.result);
        }
        else
        {
            Debug.Log("Result:No Combinations Found");
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
