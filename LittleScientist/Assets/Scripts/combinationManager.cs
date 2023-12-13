using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

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
    public GameObject discoveryInsideRecObj;


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
    public GameObject leftScrollView;
    public GameObject RightScrollView;
    public bool elementCreated = false;
    public string posString;
    public string FinalposString;
    public string loadedString;
    public Vector2 newCreatedElementPos;
    public Vector2 savednewCreatedElementPos;
    public string elementFound;

    public Vector2 loadedPosition;
    public Vector2 finalPosition;
    public string savedPositionValue;
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
    public List<string> SavedPositions = new List<string>();
    public List<string> SavedPositionsForNewCreatedElements = new List<string>();
    public List<string> FinalSavedPosition = new List<string>();

    [Header("Collecting GameObjects-New Created Elements")]
    public GameObject[] NewCreatedElementsPresent;


    [Header("Arrays")]
    public GameObject[] elementsPanelObj = new GameObject[5];

    [Header("FileData")]
    private string saveFilePath = "CreatedElements.txt";
    private string saveElementPositions = "saveElementPositions.txt";

    public enum ElementState
    {
        InitialState,NewElementFound,ElementExists,NoCombinationFound
    };

    public ElementState currentElementState = ElementState.InitialState;


    private void Start()
    {
        elementLoaderObj = FindObjectOfType<ElementLoader>();
        elementsPanel = GameObject.Find("AllElements");
        topScrollView = GameObject.Find("TopScroll_Content");
        BottomScrollView = GameObject.Find("DownScroll_Content");
        leftScrollView = GameObject.Find("LeftScroll_Content");
        RightScrollView = GameObject.Find("RightScroll_Content");
        PlayerPrefs.SetInt("elementCreated", 0);
        loadingElementPositonFromFile();
        loadCreatedElementsFromFile();
        //loadSavedCreatedElements();
        LoadDisabledGameObjectsList();
        currentElementState = ElementState.InitialState;
    }

    private void Update()
    {
        switchingOffElements();
        //Fetching All Created List
        LoadCreatedElementList();
        tempNewCreatedObj = GameObject.FindGameObjectsWithTag("NewCreatedElement");
        
        tempCopiedCreatedObj = GameObject.FindGameObjectsWithTag("Copied");
        NewCreatedElementsPresent = GameObject.FindGameObjectsWithTag("NewCreatedElement");
    }


    public void HandleCombination(string element1,string element2)
    {
        if (GameOperation._Instance.GameState == GameState.Playing)
        {
            COM_Element2 = element2;
            COM_Element1 = element1;
            resultCombination = FindCombination(COM_Element1, COM_Element2);
            currentElementState = ElementState.InitialState;
            if (resultCombination != null)
            {
                Debug.Log("Result:" + resultCombination.result);
                //PlayerPrefs.SetString("parentElement1", COM_Element1);
                //PlayerPrefs.SetString("parentElement2", COM_Element2);
                if (!loadCreatedElements.Contains(resultCombination.result))
                {
                   
                    createNewElement();
                    

                }
                else
                {
                    Debug.Log("Combination already Present");
                    PlayerPrefs.SetInt("ElementAlreadyPresent", 1);
                    PlayerPrefs.SetString("AlreadyPresentElement",resultCombination.result);
                    //StartCoroutine(CombinationPresent());

                }
            }
            else
            {

                Debug.Log("No Combination Found");
                PlayerPrefs.SetInt("NoCombinationFound", 1);
            }
        }
    }


    public void createNewElement()
    {

        if (!CreatedElements.Contains(resultCombination.result))
        {
            CreatedElements.Add(resultCombination.result);
            currentElementState = ElementState.NewElementFound;
            for (int i = 0; i < CreatedElements.Count; i++)
            {
                PlayerPrefs.SetString("CreatedElementData" + i, CreatedElements[i]);
            }
            PlayerPrefs.SetInt("StringCount", CreatedElements.Count);
            saveCreatedElementsToFile();
            //saveCreateNewElement();
            PlayerPrefs.Save();

            StartCoroutine(OpenNewElementPanel());
        }
    }

    public void saveCreatedElementsToFile()
    {
        string FilePath = Path.Combine(Application.persistentDataPath, saveFilePath);
        try
        {
            using (FileStream fileStream = File.Open(FilePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, CreatedElements);
            }
        }
        catch(Exception e)
        {
            Debug.Log("Error While saving the Created Elements:" + e.Message);
        }
    }

    public void loadCreatedElementsFromFile()
    {
        string FilePath = Path.Combine(Application.persistentDataPath, saveFilePath);
        if(File.Exists(FilePath))
        {
            try
            {
                using(FileStream fileStream=File.Open(FilePath,FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    CreatedElements=(List<string>)binaryFormatter.Deserialize(fileStream);
                }
            }
            catch(Exception e)
            {
                Debug.Log("Error While Loading Created Elements:" + e.Message);
            }
        }
    }


    public void saveCreateNewElement()
    {
       // //For Android
       // 
       // string filePath = Path.Combine(Application.persistentDataPath, saveFilePath);
       // File.WriteAllLines(filePath, CreatedElements.ToArray());
       // 
       // //For WebGL
       // string saveData = string.Join(";", CreatedElements.ToArray());
       // PlayerPrefs.SetString("CreatedElements", saveData);
       // PlayerPrefs.Save();
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
                newObj.transform.localScale = new Vector2(1f, 1f);
                placingElementsInScrollRect();

                
                //Creating Element of Discovery Tray
                discovery_element = Instantiate(discoveryElementPrefab);
                discovery_element.name = loadedString;
                discovery_element.GetComponent<BoxCollider2D>().enabled = false;
                discovery_element.transform.localScale = new Vector2(1f, 1f);
                placingElementsInDiscoveryTray();

                // if(PlayerPrefs.GetInt("CleanedUpNewCreatedElement")==0)

                //Creating New Elements for Play Area
                   
                    InsideBox_newObj = Instantiate(newCreatedElement);
                    InsideBox_newObj.name = loadedString;
                    InsideBox_newObj.transform.parent = elementsPanel.transform;
                    GameObject[] var = GameObject.FindGameObjectsWithTag("Copied");
                    GameObject[] varNewCreatedObj = GameObject.FindGameObjectsWithTag("NewCreatedElement");
                    Debug.Log("New Element Created Pos1=" + newCreatedElementPos);
                    PlayerPrefs.SetInt("elementCreated", 1);
                   
               

                if (PlayerPrefs.GetInt("IsRestart") == 0)
                { 

                        foreach (GameObject g in var)
                        {
                            if (g.GetComponent<Element>().isCollided)
                            {
                                newCreatedElementPos = g.GetComponent<Element>().loadedVector;
                                Debug.Log("New Element Created Pos1=" + newCreatedElementPos);
                            }

                        }
                        foreach (GameObject g in varNewCreatedObj)
                        {
                            if (g.GetComponent<Element>().isCollided)
                            {
                                newCreatedElementPos = g.GetComponent<Element>().loadedVector;
                                Debug.Log("New Element Created Pos2=" + newCreatedElementPos);
                            }
                        }
                        if (newCreatedElementPos == Vector2.zero)
                        {
                            if (FindObjectOfType<ElementPosition>().newFinalPos == Vector2.zero)
                            {
                                LoadNewElementPositionFunction(loadedString);
                                InsideBox_newObj.transform.position = loadedPosition;
                                Debug.Log("Loaded Position for =" + loadedString + " Is= " + loadedPosition);
                            }
                            else
                            {
                                FindObjectOfType<ElementPosition>().GetPositonFromList();
                            }

                        }
                        else
                        {
                            InsideBox_newObj.transform.position = newCreatedElementPos;
                            Debug.Log("New Element Created Pos3=" + newCreatedElementPos);
                            newElementPositionFunction();
                            InsideBox_newObj.transform.GetChild(1).GetComponent<Image>().sprite = newObj.transform.GetChild(1).GetComponent<Image>().sprite;
                            InsideBox_newObj.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;
                        }

                }
                else
                {
                    Debug.Log("after Restart");
                    LoadNewElementPositionFunction(loadedString);
                    InsideBox_newObj.transform.position = loadedPosition;
                    InsideBox_newObj.transform.GetChild(1).GetComponent<Image>().sprite = newObj.transform.GetChild(1).GetComponent<Image>().sprite;
                    InsideBox_newObj.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;
                }
                //Element Inside Play area
                Sprite elementImage = LoadElementImage(loadedString);
                if (elementImage != null)
                {
                    //Inside Ring Element
                    newObj.transform.GetChild(1).GetComponent<Image>().sprite = elementImage;
                    newObj.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;
                
                    discovery_element.GetComponent<Image>().sprite = elementImage;
                    discovery_element.GetComponent<Image>().preserveAspect = true;
                
                    NewelementImage.sprite = elementImage;
                    newElementText.text = loadedString;
                }
                //Instance_Element.isColliding = false;
                int childinLeftScrollLocal = leftScrollView.transform.childCount;
                if (childinLeftScrollLocal >= 5)
                {
                    break;
                }
                int childInTopScrolllocal = topScrollView.transform.childCount;
                if (childInTopScrolllocal >= 8)
                {
                    break;
                }

               

            }
            FindObjectOfType<ElementManager>().DisablingObjects();
        }

    }

    public void SpawningDiscoveryElementInsideRect()
    {
        //Creating element from Discovery Tray to Rectangle
        string discoveryElementName = PlayerPrefs.GetString("DiscoveryElementSelected");
        if (!string.IsNullOrEmpty(discoveryElementName))
        {
            if (leftScrollView.transform.childCount < 4)
            {
                discoveryInsideRecObj = Instantiate(InsideRingElement);
                discoveryInsideRecObj.name = discoveryElementName;
                discoveryInsideRecObj.GetComponent<BoxCollider2D>().enabled = false;
                discoveryInsideRecObj.transform.localScale = new Vector2(1f, 1f);
                Sprite discoveryElementImageInsideRect = LoadElementImageOfDiscoveryElement(discoveryElementName);
                placingDiscoveryElementInScrollRect();
                if (discoveryElementImageInsideRect != null)
                {
                    discoveryInsideRecObj.transform.GetChild(1).GetComponent<Image>().sprite = discoveryElementImageInsideRect;
                    discoveryInsideRecObj.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;
                }
            }
            else
            {
                discoveryInsideRecObj = Instantiate(InsideRingElement);
                discoveryInsideRecObj.name = discoveryElementName;
                discoveryInsideRecObj.GetComponent<BoxCollider2D>().enabled = false;
                discoveryInsideRecObj.transform.localScale = new Vector2(1f, 1f);
                Sprite discoveryElementImageInsideRect = LoadElementImageOfDiscoveryElement(discoveryElementName);
                placingDiscoveryElementInScrollRect();
                if (discoveryElementImageInsideRect != null)
                {
                    discoveryInsideRecObj.transform.GetChild(1).GetComponent<Image>().sprite = discoveryElementImageInsideRect;
                    discoveryInsideRecObj.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;
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
        else if(BottomScrollView.transform.childCount<8)
        {
            newObj.transform.position = BottomScrollView.transform.position;
            newObj.transform.parent = BottomScrollView.transform;
        }
    }

    public void placingDiscoveryElementInScrollRect()
    {

        if(leftScrollView.transform.childCount<4)
        {
            discoveryInsideRecObj.transform.position = leftScrollView.transform.position;
            discoveryInsideRecObj.transform.parent = leftScrollView.transform;

        }
        else if(RightScrollView.transform.childCount<4)
        {
            discoveryInsideRecObj.transform.position = RightScrollView.transform.position;
            discoveryInsideRecObj.transform.parent = RightScrollView.transform;
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
            PlayerPrefs.SetString(loadedString, PosString);
            savedPositionValue = PlayerPrefs.GetString(loadedString);
            Debug.Log("Saved Position value="+savedPositionValue);

            if(!SavedPositions.Contains(loadedString+":"+savedPositionValue))
            {
                SavedPositions.Add(loadedString+":"+savedPositionValue);
                savingElementPositionToFile();
            }
        }
    }

    public void savingElementPositionToFile()
    {
        string FilePath = Path.Combine(Application.persistentDataPath, saveElementPositions);
        try
        {
            using (FileStream fileStream = File.Open(FilePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, SavedPositions);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error While saving the Elements Positions:" + e.Message);
        }
    }

    public void loadingElementPositonFromFile()
    {
        string FilePath = Path.Combine(Application.persistentDataPath, saveElementPositions);
        if (File.Exists(FilePath))
        {
            try
            {
                using (FileStream fileStream = File.Open(FilePath, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    SavedPositions = (List<string>)binaryFormatter.Deserialize(fileStream);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Error While Loading Elements Position:" + e.Message);
            }
        }
    }

    public void LoadNewElementPositionFunction(string elementKey)
    {
        //string savedPosString = PlayerPrefs.GetString(elementKey);
        //if (!string.IsNullOrEmpty(savedPosString))
        //{
        //    //Debug.Log("Value" + PlayerPrefs.GetString(elementKey));
        //    loadedPosition = convertStringToVector(savedPosString);
        //    Debug.Log("Loaded Position for New Created Element" + elementKey +":"+loadedPosition) ;
        //}
        foreach (string str in SavedPositions)
        {
            string[] parts = str.Split(':');
            if (parts.Length >= 2 && parts[0] == elementKey)
            {
                loadedPosition = convertStringToVector(parts[1]);
                break;
            }
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
            PlayerPrefs.SetInt("CleanedUpNewCreatedElement", 1);
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
    public Sprite LoadElementImageOfDiscoveryElement(string elemenName)
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