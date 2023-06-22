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

    [Header("Prefabs")]
    public GameObject newCreatedElement;
    public GameObject InsideRingElement;
    public GameObject Copied;

    [Header("Element Created Through Result")]
    public GameObject newObj;
    public GameObject InsideBox_newObj;
    public Vector2 NewCreatedElementPos;

    [Header("Panels")]
    public Sprite[] elementImages;
    public GameObject newElementCreatedPanel;
    //public Text CollidingResult;
    public Image NewelementImage;
    public Text newElementText;
    public GameObject noCombinationFoundPanel;
    public GameObject combinationAlreadyMadePanel;
    public GameObject ContentPanel;


    [Header("Public Fields")]
    public GameObject elementsPanel;
    public GameObject topScrollView;
    public GameObject BottomScrollView;
    public bool elementCreated=false;
    public string posString;
    public string FinalposString;
    public string loadedString;
    public Vector3 newCreatedElementPos;
    public Vector3 loadedPosition;
    public Vector3 finalPosition;
    public GameObject[] tempNewCreatedObj;


    [Header("Bools")]
    public bool iscombinationfound = false;


    [Header("Lists")]
    public List<string> intialElements = new List<string>();
    public List<string> CreatedElements = new List<string>();
    public List<string> loadCreatedElements = new List<string>();

    [Header("Arrays")]
    public GameObject[] elementsPanelObj=new GameObject[5];

    [Header("Transform/Transform Dictionary")]
    public Dictionary<string, Vector2> objectPositions=new Dictionary<string, Vector2>();
    public Vector2 finalPositionOfElement;

    private void Start()
    {
        elementLoaderObj = FindObjectOfType<ElementLoader>();
        elementsPanel = GameObject.Find("ElementsPanel");
        topScrollView = GameObject.Find("TopScroll_Content");
        BottomScrollView = GameObject.Find("DownScroll_Content");

    }

    private void Update()
    {
        /*
        getPosition();
        LoadCreatedElementList();
        //Elements Name coming from collision
        COM_Element1 =PlayerPrefs.GetString("element1");
        COM_Element2= PlayerPrefs.GetString("element2");
        Instance_Element = FindObjectOfType<Element>();
        resultCombination = FindCombination(COM_Element1, COM_Element2);
        if (resultCombination != null)
        {
            Debug.Log("Result:" + resultCombination.result);
            PlayerPrefs.SetString("parentElement1", COM_Element1);
            //Debug.Log("ParentElement1" + PlayerPrefs.GetString("parentElement1"));
            PlayerPrefs.SetString("parentElement2", COM_Element2);
            //Debug.Log("ParentElement2" + PlayerPrefs.GetString("parentElement2"));
            if (!loadCreatedElements.Contains(resultCombination.result))
            {
                createNewElement();

            }
            
        }
        else
        {
            Debug.Log("Result:No Combinations Found ");
        }
        tempNewCreatedObj = GameObject.FindGameObjectsWithTag("NewCreatedElement");
        PlayerPrefs.Save();  
        */
        LoadCreatedElementList();
    }
   
    public void handleCombination(string element1,string element2)
    {
        resultCombination = FindCombination(element1, element2);
        if(resultCombination!=null)
        {
            Debug.Log("Result:" + resultCombination.result);
            createNewElement();
            
        }
        else
        {
            Debug.Log("Not combination Found");
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
                loadedString = PlayerPrefs.GetString("CreatedElementData" + i);
                NewCreatedElementPos = FindObjectOfType<Element>().thisElementPosition;
                if(NewCreatedElementPos != null)
                {
                    saveObjectPosition(loadedString, NewCreatedElementPos);
                    finalPositionOfElement = getObjectPosition(loadedString);
                    Debug.Log("New Created Element Position=" + finalPositionOfElement);
                }
            }
            StartCoroutine(OpenNewElementPanel());
            PlayerPrefs.SetInt("StringCount", CreatedElements.Count);
        }
    }

    public void saveObjectPosition(string key,Vector2 position)
    {
        if(objectPositions.ContainsKey(key))
        {
            objectPositions[key] = position;
        }
        else
        {
            objectPositions.Add(key, position);
        }
    }

    public Vector2 getObjectPosition(string key)
    {
        if(objectPositions.ContainsKey(key))
        {
            return objectPositions[key];
        }
        Debug.LogWarning("Object position with key" + loadedString + "does not exist");
        return Vector2.zero;
    }


    public void LoadCreatedElementList()
    {
        int count = PlayerPrefs.GetInt("StringCount");
        Debug.Log("Count" + count);
        for (int i=0;i<count;i++)
        {
            if (!loadCreatedElements.Contains(loadedString))
            {
                loadCreatedElements.Add(loadedString);
                newObj = Instantiate(InsideRingElement);                
                newObj.name = loadedString;
                newObj.GetComponent<BoxCollider2D>().enabled = false;
                if (topScrollView.transform.childCount<8)
                {
                    newObj.transform.position = topScrollView.transform.position;
                    newObj.transform.parent = topScrollView.transform;
                }
                else
                {
                    newObj.transform.position = BottomScrollView.transform.position;
                    newObj.transform.parent = BottomScrollView.transform;
                }
                Sprite elementImage = LoadElementImage(loadedString);
                if (elementImage != null)
                {
                    //Inside Ring Element
                    newObj.GetComponent<Image>().sprite = elementImage;
                    newObj.GetComponent<Image>().preserveAspect = true;

                    NewelementImage.sprite = elementImage;
                    newElementText.text = loadedString;
                }
                int childInTopScrolllocal = topScrollView.transform.childCount;
                if (childInTopScrolllocal >= 8)
                {
                    break;
                }


                //New Created Element in Play Area
                InsideBox_newObj = Instantiate(newCreatedElement);
                InsideBox_newObj.name = loadedString;
                InsideBox_newObj.transform.parent = elementsPanel.transform;
                InsideBox_newObj.transform.position = finalPositionOfElement;
                InsideBox_newObj.GetComponent<Image>().sprite = newObj.GetComponent<Image>().sprite;
                InsideBox_newObj.GetComponent<Image>().preserveAspect = true;
                //Element Inside Play area

            }

        }
        
    }



    public void CleanUpTable()
    {
        foreach (GameObject g in tempNewCreatedObj)
        {
            g.gameObject.SetActive(false);
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

    IEnumerator OpenNewElementPanel()
    {
        newElementCreatedPanel.SetActive(true);
        SoundManager._instance.newElementCreatedSound();
        yield return new WaitForSeconds(2f);
        newElementCreatedPanel.SetActive(false);
    }
    IEnumerator NoCombinationFound()
    {
        noCombinationFoundPanel.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        noCombinationFoundPanel.SetActive(false);
    }

    IEnumerator CombinationAlreadyMade()
    {
        combinationAlreadyMadePanel.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        combinationAlreadyMadePanel.SetActive(false);
    }
    IEnumerator playnewelementSound()
    {
        yield return new WaitForSeconds(SoundManager._instance.ElementsCollideSound.clip.length);
        SoundManager._instance.newElementCreatedSound();
    }
}
