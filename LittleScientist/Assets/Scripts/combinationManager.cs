using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combinationManager : MonoBehaviour
{
    Combination resultCombination;
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

        resultCombination=FindCombination(COM_Element1,COM_Element2);
        if(resultCombination!=null)
        {
            Debug.Log("Result:" + resultCombination.result);
        }
        else
        {
            Debug.Log("No Combinations Found");
        }
    }

    private Combination FindCombination(string el1,string el2)
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

}
