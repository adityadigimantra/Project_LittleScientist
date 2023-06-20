/*
                //Creating Instance of NewObj for Inside Ring
                GameObject Instance_NewObjIR = Instantiate(newObj,Instance_Element.ELE_Element1Pos, Quaternion.identity);
                Instance_NewObjIR.transform.parent = elementsPanel.transform;
                Instance_NewObjIR.name = loadedString;
                Instance_NewObjIR.GetComponent<BoxCollider2D>().enabled = false;
                */

/*
//Creating Instance of NewObj for DownPanel
GameObject Instance_NewObj = Instantiate(newObj, ContentPanel.transform.position, Quaternion.identity);
Instance_NewObj.transform.parent = ContentPanel.transform;
Instance_NewObj.name = loadedString;
Instance_NewObj.GetComponent<BoxCollider2D>().enabled = false;
*/

/*
if (i < elementsPanelObj.Length && elementsPanelObj[i] != null)
{
    if (elementsPanelObj[i].transform.childCount == 0)
    {
        newObj.transform.position = elementsPanelObj[i].transform.position;
        newObj.transform.parent = elementsPanelObj[i].transform;

    }
    else
    {
        //finding the next elementPanelObj without a Child
        int nextIndex = findNextAvailableChildIndex(i);
        if (nextIndex != -1)
        {
            newObj.transform.position = elementsPanelObj[nextIndex].transform.position;
            newObj.transform.parent = elementsPanelObj[nextIndex].transform;
        }
        else
        {
            Debug.LogWarning("No ElementPanelObj without a Child Available");
            continue;
        }
    }
}
*/


/*
                    //Giving Image to Instance of New Obj Down
                    Instance_NewObj.GetComponent<Image>().sprite = elementImage;
                    Instance_NewObj.GetComponent<Image>().preserveAspect = true;

                    //Giving Image to Instance of New Obj Inside Ring
                    Instance_NewObjIR.GetComponent<Image>().sprite = elementImage;
                    Instance_NewObjIR.GetComponent<Image>().preserveAspect = true;
                    */