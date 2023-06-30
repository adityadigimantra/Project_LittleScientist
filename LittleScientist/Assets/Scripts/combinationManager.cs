using System.IO;
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
    public GameObject discoveryElementPrefab;

    [Header("Element Created Through Result")]
    public GameObject newObj;
    public GameObject InsideBox_newObj;
    public GameObject discovery_element;

    [Header("Panels")]
    public Sprite[] elementImages;
    public GameObject newElementCreatedPanel;
    //public Text CollidingResult;
    public Image NewelementImage;
    public Text newElementText;
    public GameObject noCombinationFoundPanel;
    public GameObject combinationAlreadyMadePanel;
    public GameObject ContentPanel;
    public GameObject discoveryTray_Panel;





    [Header("Public Fields")]
    public GameObject elementsPanel;
    public GameObject topScrollView;
    public GameObject BottomScrollView;
    public bool elementCreated = false;
    public string posString;
    public string FinalposString;
    public string loadedString;
    public Vector2 newCreatedElementPos;
    public Vector2 savednewCreatedElementPos;
    public string elementFound;

    public Vector2 loadedPosition;
    public Vector2 finalPosition;
    public GameObject[] tempNewCreatedObj;
    public GameObject[] tempCopiedCreatedObj;


    [Header("Bools")]
    public bool iscombinationfound = false;
    public bool panelShown = false;


    [Header("Lists")]
    public List<string> intialElements = new List<string>();
    public List<string> CreatedElements = new List<string>();
    public List<string> loadCreatedElements = new List<string>();
    public List<string> disabledGameobjects = new List<string>();
    public List<string> NoCombinationFoundElements = new List<string>();

    [Header("Arrays")]
    public GameObject[] elementsPanelObj = new GameObject[5];

    [Header("FileData")]
    private string saveFilePath = "CreatedElements.txt";


    private void Start()
    {
        elementLoaderObj = FindObjectOfType<ElementLoader>();
        elementsPanel = GameObject.Find("ElementsPanel");
        topScrollView = GameObject.Find("TopScroll_Content");
        BottomScrollView = GameObject.Find("DownScroll_Content");
        loadSavedCreatedElements();
        LoadDisabledGameObjectsList();

    }

    private void Update()
    {
        //Fetching All Created List
        LoadCreatedElementList();
        switchingOffElements();
        /*
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
                iscombinationfound = true;
            }
            else
            {
                iscombinationfound = false;
            }

        }
        else
        {
           // Debug.Log("Result:No Combinations Found ");
        }

        */
        tempNewCreatedObj = GameObject.FindGameObjectsWithTag("NewCreatedElement");
        tempCopiedCreatedObj = GameObject.FindGameObjectsWithTag("Copied");
    }

    public void HandleCombination(string element1,string element2)
    {
            COM_Element2 = element2;
            COM_Element1 = element1;
            resultCombination = FindCombination(element1, element2);
            if (resultCombination != null)
            {
                Debug.Log("Result:" + resultCombination.result);
                PlayerPrefs.SetString("parentElement1", element1);
                PlayerPrefs.SetString("parentElement2", element2);
                if (!loadCreatedElements.Contains(resultCombination.result))
                {
                    createNewElement();

                }
                else
                {
                    Debug.Log("Combination already Present");
                    StartCoroutine(CombinationPresent());
                    
                }
            }
            else
            {

                Debug.Log("No Combination Found");
                StartCoroutine(NoCombinationFound());

            }
            Debug.Log("Parent Element1" + PlayerPrefs.GetString("parentElement1"));
            Debug.Log("Parent Element2" + PlayerPrefs.GetString("parentElement2"));
        
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
            saveCreateNewElement();
            PlayerPrefs.Save();
        }
    }

    public void saveCreateNewElement()
    {
        //For Android
        /*
        string filePath = Path.Combine(Application.persistentDataPath, saveFilePath);
        File.WriteAllLines(filePath, CreatedElements.ToArray());
        */
        //For WebGL
        string saveData = string.Join(";", CreatedElements.ToArray());
        PlayerPrefs.SetString("CreatedElements", saveData);
        PlayerPrefs.Save();
    }
    public void loadSavedCreatedElements()
    {
        if(PlayerPrefs.HasKey("CreatedElements"))
        {
            string savedData = PlayerPrefs.GetString("CreatedElements");
            CreatedElements.AddRange(savedData.Split(';'));
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
    IEnumerator CombinationPresent()
    {
        combinationAlreadyMadePanel.SetActive(true);
        yield return new WaitForSeconds(1f);
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
       // Debug.Log("Count" + count);
        for (int i = 0; i < count; i++)
        {
            loadedString = PlayerPrefs.GetString("CreatedElementData" + i);
            //Debug.Log("Created Element Data String "+loadedString);

            if (!loadCreatedElements.Contains(loadedString))
            {
                loadCreatedElements.Add(loadedString);
                newObj = Instantiate(InsideRingElement);
                newObj.name = loadedString;
                newObj.GetComponent<BoxCollider2D>().enabled = false;
                newObj.transform.localScale = new Vector2(1f,1f);
                placingElementsInScrollRect();



                //Creating Element of Discovery Tray
                discovery_element = Instantiate(discoveryElementPrefab);
                discovery_element.name = loadedString;
                discovery_element.GetComponent<BoxCollider2D>().enabled = false;
                discovery_element.transform.localScale = new Vector2(1f, 1f);
                placingElementsInDiscoveryTray();


                //Creating New Elements for Play Area
                InsideBox_newObj = Instantiate(newCreatedElement);
                InsideBox_newObj.name = newObj.name;
                InsideBox_newObj.transform.parent = elementsPanel.transform;
                

                PlayerPrefs.SetInt("elementCreated", 1);
                GameObject[] var = GameObject.FindGameObjectsWithTag("Copied");
                if(PlayerPrefs.GetInt("IsRestart") ==0)
                {
                    foreach (GameObject g in var)
                    {
                        if (g.GetComponent<Element>().isCollided)
                        {
                            newCreatedElementPos = g.GetComponent<Element>().loadedVector;
                        }
                    }
                    InsideBox_newObj.transform.position = newCreatedElementPos;
                    InsideBox_newObj.GetComponent<Image>().sprite = newObj.GetComponent<Image>().sprite;
                    InsideBox_newObj.GetComponent<Image>().preserveAspect = true;
                    newElementPositionFunction();
                }
                else
                {
                    Debug.Log("after Restart");                   
                    LoadNewElementPositionFunction(loadedString + "1");
                    InsideBox_newObj.transform.position = loadedPosition;
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
                    discovery_element.GetComponent<Image>().sprite = elementImage;
                    discovery_element.GetComponent<Image>().preserveAspect = true;

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
    
    public void switchingOffElements()
    {
        foreach (string sameElement in CreatedElements)
        {
            if (disabledGameobjects.Contains(sameElement))
            {
                Debug.Log("Found Element" + sameElement);
                elementFound = sameElement;
                GameObject[] CopiedTypeObj = GameObject.FindGameObjectsWithTag("Copied");
                foreach(GameObject g in CopiedTypeObj)
                {
                    if(g.name==elementFound)
                    {
                        if(g.GetComponent<Element>().isCollided)
                        {
                            g.SetActive(false);
                        }
                        
                    }
                }
                GameObject[] NewTypeObj = GameObject.FindGameObjectsWithTag("NewCreatedElement");

                foreach (GameObject g in NewTypeObj)
                {
                    if (g.name == elementFound)
                    {
                     g.SetActive(false);
                    }
                }
                
            }
        }
    }
    
    public void placingElementsInScrollRect()
    {
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
    }

    public void placingElementsInDiscoveryTray()
    {
        discovery_element.transform.position = discoveryTray_Panel.transform.position;
        discovery_element.transform.parent = discoveryTray_Panel.transform;
    }

    public void newElementPositionFunction()
    {
        //Saving Position for New Created Element
        if(newCreatedElementPos!=null)
        {
            savednewCreatedElementPos = newCreatedElementPos;
            string PosString = ConvertVectorToString(savednewCreatedElementPos);
            PlayerPrefs.SetString(loadedString + "1", PosString);
            Debug.Log("Before PlayerPrefs.SetString: loadedString = " + loadedString + "1, PosString = " + PosString);
            Debug.Log("After PlayerPrefs.SetString: Key = " + loadedString + "1, Value = " + PlayerPrefs.GetString(loadedString + "1"));
            PlayerPrefs.Save();

        }
    }

    public void LoadNewElementPositionFunction(string elementKey)
    {
        string savedPosString = PlayerPrefs.GetString(elementKey);
        if (!string.IsNullOrEmpty(savedPosString))
        {
            //Debug.Log("Value" + PlayerPrefs.GetString(elementKey));
            loadedPosition = convertStringToVector(savedPosString);
            Debug.Log("Loaded Position for New Created Element" + elementKey +":"+loadedPosition) ;
        }
    }

    public string ConvertVectorToString(Vector2 vector)
    {
        return vector.x.ToString() + "," + vector.y.ToString();
    }
    public Vector2 convertStringToVector(string vectorString)
    {
        float x = 0f;
        float y = 0f;
        string[] components = vectorString.Split(',');
        bool parseSuccess = false;
        if (components.Length >= 2)
        {
            parseSuccess = float.TryParse(components[0], out x) && float.TryParse(components[1], out y);

        }
        if (parseSuccess)
        {
            Vector2 vector = new Vector2(x, y);
            return vector;
        }
        else
        {
            Debug.LogError("Failed to parse Vector2 from string: " + vectorString);
            return Vector2.zero;

        }
    }

    
    public void LoadDisabledGameObjectsList()
    {
        if (PlayerPrefs.HasKey("DisabledCollidedGameObject"))
        {
            string saveDisObj = PlayerPrefs.GetString("DisabledCollidedGameObject");
            FindObjectOfType<combinationManager>().disabledGameobjects.AddRange(saveDisObj.Split(';'));
        }
    }
    
    
    public void CleanUpTable()
    {
        foreach (GameObject g in tempNewCreatedObj)
        {
            g.gameObject.SetActive(false);
        }
        foreach(GameObject g in tempCopiedCreatedObj)
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


    public void CheckElementPresence(string el1,string el2)
    {
        el1 = PlayerPrefs.GetString("element1");
        el2 = PlayerPrefs.GetString("element2");
    }
}