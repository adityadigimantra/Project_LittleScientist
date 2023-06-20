using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class combinationManager : MonoBehaviour
{
    [Header("Instances")]
    public Combination resultCombination;
    public ElementLoader elementLoaderObj;
    public Element Instance_Element;

    [Header("Colliding Elements Name")]
    public string COM_Element1;
    public string COM_Element2;
    public bool isCreatingElement = true;

    [Header("Prefabs")]
    public GameObject prefab;
    public GameObject newCreatedElement;
    public GameObject Copied;

    [Header("Element Created Through Result")]
    public GameObject newObj;

    [Header("Ring Element Images for Transforms/Data")]
    public Sprite[] elementImages;
    public GameObject newElementCreatedPanel;
    //public Text CollidingResult;
    public Image NewelementImage;
    public GameObject ContentPanel;
    [Header("Public Fields")]
    public GameObject elementsPanel;
    public GameObject topScrollView;
    public GameObject BottomScrollView;
    [Header("Lists")]
    public List<string> intialElements = new List<string>();
    public List<string> CreatedElements = new List<string>();
    public List<string> loadCreatedElements = new List<string>();

    [Header("Arrays")]
    public GameObject[] elementsPanelObj=new GameObject[5];

    private void Start()
    {
        elementLoaderObj = FindObjectOfType<ElementLoader>();
        elementsPanel = GameObject.Find("ElementsPanel");
        topScrollView = GameObject.Find("TopScroll_Content");
        BottomScrollView = GameObject.Find("DownScroll_Content");

    }

    private void Update()
    {
        //Elements Name coming from collision
        COM_Element1=PlayerPrefs.GetString("element1");
        COM_Element2= PlayerPrefs.GetString("element2");
        Instance_Element = FindObjectOfType<Element>();
        //If Elements Combine and Give out Results
        resultCombination =FindCombination(COM_Element1,COM_Element2);
        if (resultCombination != null)
        {
            Debug.Log("Result:" + resultCombination.result);
            if(!loadCreatedElements.Contains(resultCombination.result))
            {
                createNewElement();
            }
            else
            {
                Debug.Log("Combination Already Made");
                LoadCreatedElementList();
            }
        }
        else
        {
            Debug.Log("Result:No Combinations Found ");
        }
    }
    


    public void createNewElement()
    {
        
        if(!CreatedElements.Contains(resultCombination.result))
        {
            CreatedElements.Add(resultCombination.result);
            for (int i=0;i<CreatedElements.Count;i++)
            {
                PlayerPrefs.SetString("CreatedElementData" + i, CreatedElements[i]);
                
            }
            StartCoroutine(OpenNewElementPanel());
            PlayerPrefs.SetInt("StringCount", CreatedElements.Count);
        }
        LoadCreatedElementList();
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
                newObj = Instantiate(newCreatedElement);
                newObj.name = loadedString;
                newObj.GetComponent<BoxCollider2D>().enabled = false;
                
                if(count<=5)
                {
                    newObj.transform.parent = topScrollView.transform;
                    newObj.transform.position = topScrollView.transform.position;
                }
                else if(count>5)
                {
                    newObj.transform.parent = BottomScrollView.transform;
                    newObj.transform.position = BottomScrollView.transform.position;
                }
                

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

                //Giving Image to Loaded Element

                Sprite elementImage = LoadElementImage(loadedString);
                if (elementImage != null)
                {
                    //Inside Ring Element
                    newObj.GetComponent<Image>().sprite = elementImage;
                    newObj.GetComponent<Image>().preserveAspect = true;
                    NewelementImage.sprite = elementImage;
                    /*
                    //Giving Image to Instance of New Obj Down
                    Instance_NewObj.GetComponent<Image>().sprite = elementImage;
                    Instance_NewObj.GetComponent<Image>().preserveAspect = true;

                    //Giving Image to Instance of New Obj Inside Ring
                    Instance_NewObjIR.GetComponent<Image>().sprite = elementImage;
                    Instance_NewObjIR.GetComponent<Image>().preserveAspect = true;
                    */
                }
            }
        }
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
