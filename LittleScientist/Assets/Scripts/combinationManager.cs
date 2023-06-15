using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class combinationManager : MonoBehaviour
{
    Combination resultCombination;
    Combination resultCombination2;
    public ElementLoader elementLoaderObj;
    public Element elementObj;
    public string COM_Element1;
    public string COM_Element2;
    public string newElementCreated;
    public bool creatingNewElement=true;
    public GameObject prefab;
    public GameObject Copied;

    [Header("New Created Element")]
    public GameObject newObj;
    public GameObject newObjOutsideRing;
    public GameObject finalObj;
    public Sprite[] elementImages;
    public GameObject newElementCreatedPanel;
    public Text elementNameText;
    public Image NewelementImage;
    public bool combinationFound = false;

    [Header("Lists")]
    public List<string> CreatedElements = new List<string>();
    public List<string> loadCreatedElements = new List<string>();

    [Header("Arrays")]
    public GameObject[] elementsPanelObj=new GameObject[5];

    private void Start()
    {
        elementLoaderObj = FindObjectOfType<ElementLoader>();
        LoadCreatedElementList();

    }

    private void Update()
    {
        //Elements Name coming from collision
        COM_Element1=PlayerPrefs.GetString("element1");
        COM_Element2= PlayerPrefs.GetString("element2");

        resultCombination =FindCombination1(COM_Element1,COM_Element2);
        if (resultCombination != null)
        {
            Debug.Log("Result:" + resultCombination.result);
            if(creatingNewElement)
            {
                
                createNewElement();
                combinationFound = true;
            }
        }
        else
        {
            Debug.Log("Result:No Combinations Found");
            combinationFound = false;
        }

    }
    


    public void createNewElement()
    {
        
        if(!CreatedElements.Contains(resultCombination.result))
        {
            CreatedElements.Add(resultCombination.result);
            elementNameText.text = resultCombination.result;
            StartCoroutine(OpenNewElementPanel());
            for (int i=0;i<CreatedElements.Count;i++)
            {
                PlayerPrefs.SetString("CreatedElementData" + i, CreatedElements[i]);
                
            }
            PlayerPrefs.SetInt("StringCount", CreatedElements.Count);
        }
        LoadCreatedElementList();
        creatingNewElement = false;

    }
    IEnumerator OpenNewElementPanel()
    {        
        newElementCreatedPanel.SetActive(true);
        SoundManager._instance.newElementCreatedSound();
        yield return new WaitForSeconds(2f);
        newElementCreatedPanel.SetActive(false);
    }
    IEnumerator playnewelementSound()
    {
        yield return new WaitForSeconds(SoundManager._instance.ElementsCollideSound.clip.length);
        SoundManager._instance.newElementCreatedSound();
    }

    public void LoadCreatedElementList()
    {
        int count = PlayerPrefs.GetInt("StringCount", 0);
        Debug.Log("Count" + count);
        for(int i=0;i<count;i++)
        {
            string loadedString = PlayerPrefs.GetString("CreatedElementData" + i);
            if(!loadCreatedElements.Contains(loadedString))
            {
                loadCreatedElements.Add(loadedString);
                newObj = Instantiate(prefab);
                Vector3 posOffset = FindObjectOfType<Element>().averagePos;
                newObjOutsideRing = Instantiate(Copied,posOffset,Quaternion.identity);
                newObj.name = loadedString;
                newObjOutsideRing.name = loadedString;
                newObj.GetComponent<BoxCollider2D>().enabled = false;
                newObjOutsideRing.GetComponent<BoxCollider2D>().enabled = true;
                GameObject panel = GameObject.Find("ElementsPanel");
                newObjOutsideRing.transform.parent = panel.transform;
                if (i<elementsPanelObj.Length && elementsPanelObj[i]!=null)
                {
                  if(elementsPanelObj[i].transform.childCount==0)
                    {
                        newObj.transform.position = elementsPanelObj[i].transform.position;
                        newObj.transform.parent = elementsPanelObj[i].transform;
                    }
                  else
                    {
                        //finding the next elementPanelObj without a Child
                        int nextIndex = findNextAvailableChildIndex(i);
                        if(nextIndex!=-1)
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
                //newObj.transform.parent = panel.transform;

                Sprite elementImage = LoadElementImage(loadedString);
                if(elementImage!=null)
                {
                    newObj.GetComponent<Image>().sprite = elementImage;
                    newObj.GetComponent<Image>().preserveAspect = true;
                    NewelementImage.sprite = elementImage;
                    newObjOutsideRing.GetComponent<Image>().sprite = elementImage;
                    newObj.GetComponent<Image>().preserveAspect = true;

                }

            }
        }
        creatingNewElement = false;
    }
    public int findNextAvailableChildIndex(int startIndex)
    {
        for(int i=startIndex;i<elementsPanelObj.Length;i++)
        {
            if(elementsPanelObj[i].transform.childCount==0)
            {
                return i;
            }
        }
        return -1;
    }

    public Sprite LoadElementImage(string elemenName)
    {
        string imagePath = "Elements/" + elemenName;
        return Resources.Load<Sprite>(imagePath);
        Debug.Log("Image Loaded");
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
