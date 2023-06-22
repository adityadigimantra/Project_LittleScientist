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




/*
// Save the position of an object
public void SaveObjectPosition(Vector3 position, string key)
{
    // Convert the position to a string
    string positionString = position.x.ToString() + "," + position.y.ToString() + "," + position.z.ToString();

    // Save the position string in PlayerPrefs
    PlayerPrefs.SetString(key, positionString);
}

// Load the position of an object
public Vector3 LoadObjectPosition(string key)
{
    // Check if the position string exists in PlayerPrefs
    if (PlayerPrefs.HasKey(key))
    {
        // Retrieve the position string from PlayerPrefs
        string positionString = PlayerPrefs.GetString(key);

        // Split the position string into individual coordinates
        string[] positionCoordinates = positionString.Split(',');

        // Convert the coordinates back to floats and create a Vector3
        float x = float.Parse(positionCoordinates[0]);
        float y = float.Parse(positionCoordinates[1]);
        float z = float.Parse(positionCoordinates[2]);
        Vector3 position = new Vector3(x, y, z);

        // Return the loaded position
        return position;
    }

    // Return a default position if the key does not exist
    return Vector3.zero;
}


Vector3 objectPosition = myObject.transform.position;
SaveObjectPosition(objectPosition, "ObjectPositionKey");

Vector3 loadedPosition = LoadObjectPosition("ObjectPositionKey");
myObject.transform.position = loadedPosition;

*/

///////////////////////////////////////////////////////////////////////////////
/////Combination Manager Original
/*
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
    public bool elementCreated = false;
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
    public GameObject[] elementsPanelObj = new GameObject[5];

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
        COM_Element1 = PlayerPrefs.GetString("element1");
        COM_Element2 = PlayerPrefs.GetString("element2");
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
    }



    public void createNewElement()
    {

        if (!CreatedElements.Contains(resultCombination.result))
        {
            CreatedElements.Add(resultCombination.result);
            for (int i = 0; i < CreatedElements.Count; i++)
            {
                PlayerPrefs.SetString("CreatedElementData" + i, CreatedElements[i]);
            }
            StartCoroutine(OpenNewElementPanel());
            PlayerPrefs.SetInt("StringCount", CreatedElements.Count);
            //Changes done here
            PlayerPrefs.Save();
        }
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

    public void LoadCreatedElementList()
    {
        int count = PlayerPrefs.GetInt("StringCount");
        Debug.Log("Count" + count);
        for (int i = 0; i < count; i++)
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

                if (topScrollView.transform.childCount < 8)
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


                //New Created Element

                //Temperary Have to remove this
                StartCoroutine(ChangePlayerPrefValue());
                if (PlayerPrefs.GetInt("IsRestart") == 0)
                {
                    InsideBox_newObj = Instantiate(newCreatedElement);
                    InsideBox_newObj.name = newObj.name;
                    InsideBox_newObj.transform.parent = elementsPanel.transform;
                    InsideBox_newObj.transform.position = finalPosition;

                    InsideBox_newObj.GetComponent<Image>().sprite = newObj.GetComponent<Image>().sprite;
                    InsideBox_newObj.GetComponent<Image>().preserveAspect = true;
                }




                //Element Inside Play area
                Sprite elementImage = LoadElementImage(loadedString);
                if (elementImage != null)
                {
                    //Inside Ring Element
                    newObj.GetComponent<Image>().sprite = elementImage;
                    newObj.GetComponent<Image>().preserveAspect = true;

                    NewelementImage.sprite = elementImage;
                    newElementText.text = loadedString;
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
    public Vector3 loadfinalPosNewCreatedElement(string key)
    {
        if (PlayerPrefs.HasKey(key))
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

    IEnumerator ChangePlayerPrefValue()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerPrefs.SetInt("IsRestart", 0);
    }
    public void getPosition()
    {
        loadedPosition = loadPositionOfElement("averagePos");
        savefinalPosNewCreatedElement(loadedPosition, loadedString);
        finalPosition = loadfinalPosNewCreatedElement(loadedString);
        loadedPosition = finalPosition;
    }

    public Vector3 loadPositionOfElement(string key)
    {
        if (PlayerPrefs.HasKey(key))
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

    public void savefinalPosNewCreatedElement(Vector3 position, string key)
    {
        FinalposString = position.x.ToString() + "," + position.y.ToString() + "," + position.z.ToString();
        PlayerPrefs.SetString(key, FinalposString);
        //Debug.Log("PlayerPref Value" + PlayerPrefs.GetString(key));
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
        for (int i = startIndex; i < elementsPanelObj.Length; i++)
        {
            if (elementsPanelObj[i].transform.childCount == 0)
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

    private Combination FindCombination(string el1, string el2)
    {
        foreach (Combination comb in elementLoaderObj.elementsList)
        {
            if (comb.element1 == el1 && comb.element2 == el2)
            {
                return comb;
            }
        }
        return null;
    }
}
*/


///////////////////////////////////////////////////////////////////////////////////
///Element.cs
/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    [Header("This Element Data")]
    public string thisElementName;
    public GameObject thisElementObj;
    public Vector2 thisElementPosition;
    public Sprite thisElementImage;

    [Header("Other Element Data")]
    public string OtherElementName;
    public GameObject otherElementObj;
    public Vector2 otherElementPosition;
    public Sprite otherElementImage;


    [Header("Element Colliding Data")]
    public GameObject[] tempObj;
    public Vector2 averagePos;
    public GameObject[] SameNameTagObj;
    public bool isColliding = false;
    public bool isCollided = false;


    [Header("Instances")]
    public combinationManager comManager;

    private void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        thisElementName = this.gameObject.name;
        thisElementObj = this.gameObject;
        thisElementImage = this.gameObject.GetComponent<Image>().sprite;
    }

    private void Update()
    {
        //Getting live Position of Both Element
        getPositionOfElements();
        SameNameTagObj = GameObject.FindGameObjectsWithTag("Copied");
        DestroyingObj();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //SoundManager._instance.elementCollideSound();
        otherElementObj = other.gameObject;
        thisElementName = thisElementObj.name;
        PlayerPrefs.SetString("element1", thisElementName);
        OtherElementName = otherElementObj.name;
        PlayerPrefs.SetString("element2", OtherElementName);
        //Debug.Log("Element 1=" + thisElementObj + "Collided with Element 2=" + otherElementObj);
        //isCollided = true;
        //PlayerPrefs.SetInt("GameStarted", 1);
        comManager.handleCombination(thisElementName, OtherElementName);
    }

    public void getPositionOfElements()
    {
        thisElementPosition = thisElementObj.GetComponent<RectTransform>().transform.position;
        if(otherElementObj != null)
        {
            otherElementPosition = otherElementObj.GetComponent<RectTransform>().transform.position;
            averagePos = (thisElementPosition + otherElementPosition) / 2;
            savePositionofElements(averagePos,"averagePos");
        }
    }

    public void DestroyingObj()
    {
        if(PlayerPrefs.GetInt("elementCreated")==1)
        {
            //To Search all the Elements with same Name Present in the Scene
            foreach(GameObject g in SameNameTagObj)
            {
                Element element = g.GetComponent<Element>();
                if(element!=null && element.isCollided)
                {
                    //Debug.Log("GameObject " + g.name + "has collided");
                    g.SetActive(false);
                }    
            }
            PlayerPrefs.SetInt("elementCreated", 0);
        }

    }

    public void savePositionofElements(Vector3 position,string key)
    {
        string posString = position.x.ToString() + "," + position.y.ToString() + "," + position.z.ToString();
        PlayerPrefs.SetString(key, posString);
    }
    

}
*/