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
    public bool elementCreated=false;
    public string posString;
    public string FinalposString;
    public string loadedString;
    public Vector3 newCreatedElementPos;
    public Vector3 loadedPosition;
    public Vector3 finalPosition;


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
        getPosition();
        LoadCreatedElementList();
        //Elements Name coming from collision
        COM_Element1 =PlayerPrefs.GetString("element1");
        COM_Element2= PlayerPrefs.GetString("element2");
        Instance_Element = FindObjectOfType<Element>();

        //If Elements Combine and Give out Results
        resultCombination =FindCombination(COM_Element1,COM_Element2);
        if (resultCombination != null)
        {
            Debug.Log("Result:" + resultCombination.result);
            PlayerPrefs.SetString("parentElement1", COM_Element1);
            Debug.Log("ParentElement1" + PlayerPrefs.GetString("parentElement1"));
            PlayerPrefs.SetString("parentElement2", COM_Element2);
            Debug.Log("ParentElement2" + PlayerPrefs.GetString("parentElement2"));
            if (!loadCreatedElements.Contains(resultCombination.result))
            {
                createNewElement();
               
            }
            else
            {
               
                Debug.Log("Combination Already Made");
            }
        }
        else
        {
            Debug.Log("Result:No Combinations Found ");
        }
        PlayerPrefs.Save();
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
        for (int i=0;i<count;i++)
        {
            loadedString = PlayerPrefs.GetString("CreatedElementData" + i);
            //Debug.Log("Created Element Data String "+loadedString);

            if (!loadCreatedElements.Contains(loadedString))
            {
                loadCreatedElements.Add(loadedString);
                newObj = Instantiate(InsideRingElement);                
                newObj.name = loadedString;
                PlayerPrefs.SetInt("elementCreated", 1);
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
                elementCreated = true;

                InsideBox_newObj = Instantiate(newCreatedElement);
                InsideBox_newObj.name = newObj.name;
                InsideBox_newObj.transform.parent = elementsPanel.transform;
                InsideBox_newObj.transform.position = finalPosition;
                
                InsideBox_newObj.GetComponent<Image>().sprite = newObj.GetComponent<Image>().sprite;
                InsideBox_newObj.GetComponent<Image>().preserveAspect = true;
                


                //Element Inside Play area
                Sprite elementImage = LoadElementImage(loadedString);
                if (elementImage != null)
                {
                    //Inside Ring Element
                    newObj.GetComponent<Image>().sprite = elementImage;
                    newObj.GetComponent<Image>().preserveAspect = true;
                   
                    NewelementImage.sprite = elementImage;
                }

                //Instance_Element.isColliding = false;
                int childInTopScrolllocal = topScrollView.transform.childCount;
                if (childInTopScrolllocal >= 8)
                {
                    break;
                }
            }

        }
        
    }
    public void getPosition()
    {
       loadedPosition = loadPositionOfElement("averagePos");
       savefinalPosNewCreatedElement(loadedPosition, loadedString);
       finalPosition=loadfinalPosNewCreatedElement(loadedString);
       loadedPosition = finalPosition;
    }

    public Vector3 loadPositionOfElement(string key)
    {
        if(PlayerPrefs.HasKey(key))
        {
            posString = PlayerPrefs.GetString(key);
            string[] posCordinates = posString.Split(',');
            float x = float.Parse(posCordinates[0]);
            float y = float.Parse(posCordinates[1]);
            float z = float.Parse(posCordinates[2]);

            Vector3 localPos = new Vector3(x, y, z);
            return localPos;
        }
        return Vector3.zero;

    }

    public void savefinalPosNewCreatedElement(Vector3 position,string key)
    {
        FinalposString = position.x.ToString() + "," + position.y.ToString() + "," + position.z.ToString();
        PlayerPrefs.SetString(key, FinalposString);
        //Debug.Log("PlayerPref Value" + PlayerPrefs.GetString(key));
    }

    public Vector3 loadfinalPosNewCreatedElement(string key)
    {
        if(PlayerPrefs.HasKey(key))
        {
            FinalposString = PlayerPrefs.GetString(key);
            string[] positionCordinates = FinalposString.Split(',');
            float x = float.Parse(positionCordinates[0]);
            float y = float.Parse(positionCordinates[1]);
            float z = float.Parse(positionCordinates[2]);
            Vector3 localPosition = new Vector3(x, y, z);
            return localPosition;
        }
        return Vector3.zero;
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
