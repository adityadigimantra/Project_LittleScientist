using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.UI.Extensions;
using System.Linq;

public class combinationManager : MonoBehaviour
{
    [Header("Instances")]
    public Combination resultCombination;
    public ElementLoader elementLoaderObj;
    public Element Instance_Element;
    public CharacterManager charManager;
    public ElementManager elementManager;
    public CharacterMessages charMessages;
    public SoundManager soundManager;


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

    [Header("Discovery Trays")]
    public GameObject def_discoveryTray_Panel;
    public GameObject for_discoveryTray_Panel;
    public GameObject aqua_discoveryTray_Panel;


    [Header("FunFacts")]
    public GameObject[] FunFactsBar;
    public Text FunfactText;
    private string funfactstr;
    public int factImageBarIndex;
    public int funFactTextSize;


    [Header("Public Fields")]
    public GameObject def_topScrollView;
    public GameObject def_BottomScrollView;

    [Header("Theme Elements Panel")]
    public GameObject def_elementsPanel;
    public GameObject for_elementsPanel;
    public GameObject aqua_elementsPanel;

    [Header("Forest Scrolls Views")]
    public GameObject for_topScrollView;
    public GameObject for_BottomScrollView;

    [Header("Aqua Scrolls Views")]
    public GameObject aqua_topScrollView;
    public GameObject aqua_BottomScrollView;

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
    public List<string> elementsDraggedToTrash = new List<string>();
    public List<string> elementsDraggedToRects = new List<string>();
    public List<string> NoCombinationFoundElements = new List<string>();
    public List<string> SavedPositions = new List<string>();
    public List<string> SavedPositionsForNewCreatedElements = new List<string>();
    public List<string> FinalSavedPosition = new List<string>();
    public List<string> LeftScrollViewList = new List<string>();
    public List<string> RightScrollViewList = new List<string>();
    public List<string> InsideRinglElementsList = new List<string>();

    [Header("Collecting GameObjects-New Created Elements")]
    public GameObject[] NewCreatedElementsPresent;


    [Header("Arrays")]
    public GameObject[] elementsPanelObj = new GameObject[5];

    [Header("FileData")]
    private string saveFilePath = "CreatedElements.txt";
    private string saveElementPositions = "saveElementPositions.txt";

    [Header("Animations Data")]
    public Animator NewCreatedElementPanelAnimator;

    [Header("Messages Fields")]
    public string WelcomeMessage;
    public string NewElementFoundMessage;
    public string CombinationExistsMessage;
    public string NoCombinationExistsMessage;

    [Header("Scores Data")]
    public int score;
    public int FinalScore;
    public int previousScore;
    public int elementDiscoveredCount=0;
    public int TotalElements = 45;
    public enum ElementState
    {
        InitialState,NewElementFound,ElementExists,NoCombinationFound,IdleState
    };

    public ElementState currentElementState = ElementState.InitialState;

    [Header("Duplicated Gameobjects")]
    public GameObject[] duplicatedObjs;
    public GameObject[] switchedOffGameObjects;

    [Header("Partciles System")]
    public GameObject def_DaimondParticles;
    public GameObject for_DaimondParticles;
    public GameObject aqua_DaimondParticles;

    public string operatingSystem;
    public string osName;
    public string selectedTheme;
    private void Start()
    {
       
        //Start-Preload.
        elementLoaderObj = FindObjectOfType<ElementLoader>();
        charManager = FindObjectOfType<CharacterManager>();
        elementManager = FindObjectOfType<ElementManager>();
        charMessages = FindObjectOfType<CharacterMessages>();
        soundManager = FindObjectOfType<SoundManager>();

        //elementsPanel = GameObject.Find("AllElements");
        //def_topScrollView = GameObject.Find("Def_TopScroll_Content");
        //def_BottomScrollView = GameObject.Find("Def_DownScroll_Content");

        //for_topScrollView = GameObject.Find("for_TopScroll_Content");
        //sfor_BottomScrollView = GameObject.Find("for_DownScroll_Content");

        //aqua_topScrollView = GameObject.Find("aqua_TopScroll_Content");
        //aqua_BottomScrollView = GameObject.Find("aqua_DownScroll_Content");

        leftScrollView = GameObject.Find("LeftScroll_Content");
        RightScrollView = GameObject.Find("RightScroll_Content");
        SettingPlayerPrefsValuetoZero();

        currentElementState = ElementState.InitialState;

        //Calling Methods.

        loadingElementPositonFromFile();
        loadCreatedElementsFromFile();
        //Character Behaviour when the game Starts.
        if (PlayerPrefs.GetInt("ShownTutorial") ==1)
        {
            GiveWelcomeMessage();
        }
        //Disabled Elements List clearing and loading from File.
        //First Clearing the list of Disabled Gameobjects.
        disabledGameobjects.Clear();
        elementsDraggedToTrash.Clear();
        elementManager.GetDisabledGameObjectsList();
        elementManager.GetTrashGameObjectsList();
        elementManager.GetScrollRectGameObjectsList();
        LoadScore();
        //switchingOffElements();

    }

   
    private void Update()
    {
        selectedTheme = PlayerPrefs.GetString("Theme");
        //Fetching All Created List
        getOsName();
        LoadCreatedElementList();
        tempNewCreatedObj = GameObject.FindGameObjectsWithTag("NewCreatedElement");
        
        tempCopiedCreatedObj = GameObject.FindGameObjectsWithTag("Copied");
        NewCreatedElementsPresent = GameObject.FindGameObjectsWithTag("NewCreatedElement");

        if (PlayerPrefs.GetInt("elementCreated") == 1)
        {
            elementManager.DisablingObjects();
        }
    }

    public void getOsName()
    {
        operatingSystem = SystemInfo.operatingSystem;
        if (operatingSystem.Contains("Windows"))
        {
            osName = "Windows";
        }
        else if (operatingSystem.Contains("Mac"))
        {
            osName = "MacOS";
        }
        else
        {
            osName = "Other";
        }
        Debug.Log("Operating System: " + osName);
    }
    public void SettingPlayerPrefsValuetoZero()
    {
        PlayerPrefs.SetInt("elementCreated", 0);
        PlayerPrefs.SetInt("NoCombinationFound", 0);
        PlayerPrefs.SetInt("ElementAlreadyPresent", 0);
    }
    public void HandlingDuplicatedElements()
    {
        foreach (string name in disabledGameobjects)
        {
            GameObject foundobj = GameObject.Find(name);
            if(foundobj.tag=="NewCreatedElement")
            {
                foundobj.SetActive(false);
            }

         } 
    }

    public void HandleCombination(string element1,string element2)
    {
        if (GameOperation._Instance.GameState == GameState.Playing)
        {
            COM_Element2 = element2;
            COM_Element1 = element1;
            resultCombination = FindCombination(COM_Element1, COM_Element2);
            if (resultCombination != null)
            {
                 Debug.Log("Result:" + resultCombination.result);
                 funfactstr = resultCombination.fact;
                 CreateFunFact(funfactstr, resultCombination.fontsize, resultCombination.factimage);
                 if (string.IsNullOrEmpty(funfactstr))
                 {
                     funfactstr = "No Fun Fact Present.";
                 }
                 
                 if (!loadCreatedElements.Contains(resultCombination.result))
                 {
                     createNewElement();
                 }
                 else
                 {
                     HandlesCombinationExists();
                 }

            }
            else
            {
                 HandlesNoCombinationFound();
            }
        }
    }

    public void CreateFunFact(string fact,int fontsize,int barImageIndex)
    {
        funfactstr = fact;
        funFactTextSize = fontsize;
        factImageBarIndex = barImageIndex;
        FunfactText.text = funfactstr;
        FunfactText.fontSize = funFactTextSize;
        if(factImageBarIndex>=1 && factImageBarIndex<=FunFactsBar.Length)
        {
            for(int i=0;i<FunFactsBar.Length;i++)
            {
                FunFactsBar[i].SetActive(i == (factImageBarIndex - 1));
            }
        }

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
            PlayerPrefs.SetInt("StringCount", CreatedElements.Count);
            PlayerPrefs.SetInt("elementCreated", 1);
            CalculateScore();
            saveCreatedElementsToFile();
            PlayerPrefs.Save();
            currentElementState = ElementState.NewElementFound;
            StartCoroutine(OpenNewElementPanel());
        }
    }
    public void CalculateScore()
    {
        FinalScore += 5;
        StartCoroutine(playDaimondParticles());
        elementDiscoveredCount = PlayerPrefs.GetInt("StringCount");//Needs To Change
        SaveScore();
    }

    IEnumerator playDaimondParticles()
    {
        switch (selectedTheme)
        {
            case "Default":
                def_DaimondParticles.SetActive(true);
                yield return new WaitForSeconds(6f);
                def_DaimondParticles.SetActive(false);

                break;

            case "Forest":
                for_DaimondParticles.SetActive(true);
                yield return new WaitForSeconds(6f);
                for_DaimondParticles.SetActive(false);
                break;

            case "Aqua":
                aqua_DaimondParticles.SetActive(true);
                yield return new WaitForSeconds(6f);
                aqua_DaimondParticles.SetActive(false);
                break;
        }

        
    }
    public void SaveScore()
    {
        PlayerPrefs.SetInt("SavedScore", FinalScore);
        PlayerPrefs.SetInt("SavedElementDiscoveredCount", elementDiscoveredCount);
        PlayerPrefs.Save();//Needs To Change
    }
    private void LoadScore()//Needs To Change
    {
        if(PlayerPrefs.HasKey("SavedScore"))
        {
            FinalScore = PlayerPrefs.GetInt("SavedScore");//Needs To Change
        }
        if(PlayerPrefs.HasKey("SavedElementDiscoveredCount"))
        {
            elementDiscoveredCount = PlayerPrefs.GetInt("SavedElementDiscoveredCount");

        }
    }
    public void HandlesCombinationExists()
    {
        Debug.Log("Combination already Present");
        PlayerPrefs.SetInt("ElementAlreadyPresent", 1);
        PlayerPrefs.SetString("AlreadyPresentElement", resultCombination.result);
        soundManager.CombinationAlreadyExistsSound();
        StartCoroutine(givingDelayforMessageAnimation());
    }
    IEnumerator givingDelayforMessageAnimation()
    {
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(1);
        GiveCombinationAlreadyExistsMessage();
    }
    public void HandlesNoCombinationFound()
    {
        Debug.Log("No Combination Found");
        PlayerPrefs.SetInt("NoCombinationFound", 1);
        StartCoroutine(givingDelayForMessageAnimation_HandlesNoCombination());
        soundManager.NoCombinationFoundSound();
        //elementManager.DisablingObjects();
    }
    IEnumerator givingDelayForMessageAnimation_HandlesNoCombination()
    {
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(1);
        GiveNoCombinationFoundMessage();

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
        NewCreatedElementPanelAnimator.SetBool("IsOpen", true);
        newElementCreatedPanel.SetActive(true);
        soundManager.newElementCreatedSound();
        //closing the Previous Message Box
        charManager.CloseCurrentMessage();
        yield return new WaitForSeconds(3f);
        NewCreatedElementPanelAnimator.SetBool("IsOpen", false);
       
        elementManager.MakeGameObjectsNull();
        GiveNewElementFoundMessage();
        soundManager.playSound_Character_NewElementFound();
        currentElementState = ElementState.InitialState;
    }

    public void setScaleOfDiscoveryElements()
    {
        switch (osName)
        {
            case "Windows":
                discovery_element.transform.localScale = new Vector2(1f,1f);
                break;

            case "MacOs":
                discovery_element.transform.localScale = new Vector2(1f, 1f);
                break;

            case "Other":
                discovery_element.transform.localScale = new Vector2(1f, 1f);
                break;
        }

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
                //newObj.transform.localScale = new Vector2(1f,1f);
                if(!InsideRinglElementsList.Contains(newObj.name))
                {
                    placingElementsInScrollRect();
                }
                
                //Creating Element of Discovery Tray
                discovery_element = Instantiate(discoveryElementPrefab);
                discovery_element.name = loadedString;
 
                discovery_element.transform.GetChild(2).GetComponent<Text>().text = charManager.ConvertToUpperCase(loadedString);
                discovery_element.GetComponent<BoxCollider2D>().enabled = false;
                placingElementsInDiscoveryTray();


                // if(PlayerPrefs.GetInt("CleanedUpNewCreatedElement")==0)

                //Creating New Elements for Play Area

                InsideBox_newObj = Instantiate(newCreatedElement);
                    InsideBox_newObj.name = loadedString;
                string selectedTheme = PlayerPrefs.GetString("Theme");
                switch(selectedTheme)
                {
                    case "Default":
                        InsideBox_newObj.transform.parent = def_elementsPanel.transform;
                        break;
                    case "Forest":
                        InsideBox_newObj.transform.parent = for_elementsPanel.transform;
                        break;
                    case "Aqua":
                        InsideBox_newObj.transform.parent = aqua_elementsPanel.transform;
                        break;
                }
                   
                    //InsideBox_newObj.transform.position = elementsPanel.transform.position;

                    GameObject[] var = GameObject.FindGameObjectsWithTag("Copied");
                    GameObject[] varNewCreatedObj = GameObject.FindGameObjectsWithTag("NewCreatedElement");
                    Debug.Log("New Element Created Pos1=" + newCreatedElementPos);
                    

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
                                InsideBox_newObj.GetComponent< RectTransform>().transform.localPosition = loadedPosition;
                                Debug.Log("Loaded Position for =" + loadedString + " Is= " + loadedPosition);
                            }
                            else
                            {
                                FindObjectOfType<ElementPosition>().GetPositonFromList();
                            }

                        }
                        else
                        {
                            InsideBox_newObj.GetComponent<RectTransform>().transform.localPosition = newCreatedElementPos;
                            Debug.Log("New Element Created Pos3=" + newCreatedElementPos);
                            newElementPositionFunction();
                            InsideBox_newObj.transform.GetChild(1).GetComponent<Image>().sprite = newObj.transform.GetChild(1).GetComponent<Image>().sprite;
                            InsideBox_newObj.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;
                        }

                    LoadNewElementPositionFunction(loadedString);
                    InsideBox_newObj.GetComponent<RectTransform>().transform.localPosition = loadedPosition;
                    InsideBox_newObj.transform.GetChild(1).GetComponent<Image>().sprite = newObj.transform.GetChild(1).GetComponent<Image>().sprite;
                    InsideBox_newObj.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;
                    switchingOffElements();

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
                    string finalStringtoShowOnPanel=charManager.ConvertToUpperCase(loadedString);
                    newElementText.text = finalStringtoShowOnPanel;
                }
            }
        }
        
    }

    public void SpawningDiscoveryElementInsideRect()
    {
        //Creating element from Discovery Tray to Rectangle
        string discoveryElementName = PlayerPrefs.GetString("DiscoveryElementSelected");
        if (!string.IsNullOrEmpty(discoveryElementName))
        {
            if (!InsideRinglElementsList.Contains(discoveryElementName))
            {
                InsideRinglElementsList.Add(discoveryElementName);
                if (leftScrollView.transform.childCount < 4)
                {
                    if (!LeftScrollViewList.Contains(discoveryElementName))
                    {
                        LeftScrollViewList.Add(discoveryElementName);
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

                else
                {
                    if (!RightScrollViewList.Contains(discoveryElementName))
                    {
                        RightScrollViewList.Add(discoveryElementName);
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
        }
    }

    public void switchingOffElements()
    {
        foreach (string sameElement in loadCreatedElements)
        {
            #region Elements in DisabledGameObject list

            if (disabledGameobjects.Contains(sameElement))
            {
                Debug.Log("Found Element" + sameElement);
                elementFound = sameElement;
                GameObject[] CopiedTypeObj = GameObject.FindGameObjectsWithTag("Copied");
                foreach (GameObject g in CopiedTypeObj)
                {
                    if (g.name == elementFound)
                    {
                        if (g.GetComponent<Element>().isCollided)
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
            #endregion

            #region Elements in Trash list

            if(elementsDraggedToTrash.Contains(sameElement))
            {
                Debug.Log("Found Element Dragged To Trash" + sameElement);
                elementFound = sameElement;
                GameObject[] CopiedTypeObj = GameObject.FindGameObjectsWithTag("Copied");
                foreach (GameObject g in CopiedTypeObj)
                {
                    if (g.name == elementFound)
                    { 
                      g.SetActive(false);
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
            #endregion

            #region Elements Dragged To Rects

            if (elementsDraggedToRects.Contains(sameElement))
            {
                Debug.Log("Found Element Dragged To Trash" + sameElement);
                elementFound = sameElement;
                GameObject[] CopiedTypeObj = GameObject.FindGameObjectsWithTag("Copied");
                foreach (GameObject g in CopiedTypeObj)
                {
                    if (g.name == elementFound)
                    {
                        g.SetActive(false);
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
            #endregion
        }
    }

    public void placingElementsInScrollRect()
    {
        string selectedTheme = PlayerPrefs.GetString("Theme");
        switch (selectedTheme)
        {
            case "Default":
                if (def_topScrollView.transform.childCount < 8)
                {
                    newObj.transform.parent = def_topScrollView.transform;
                    newObj.transform.position = def_topScrollView.transform.position;
                    newObj.transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    newObj.transform.parent = def_BottomScrollView.transform;
                    newObj.transform.position = def_BottomScrollView.transform.position;
                    newObj.transform.localScale = new Vector2(1, 1);
                }
                break;
            case "Forest":
                if (for_topScrollView.transform.childCount < 8)
                {
                    newObj.transform.parent = for_topScrollView.transform;
                    newObj.transform.position = for_topScrollView.transform.position;
                    newObj.transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    newObj.transform.parent = for_BottomScrollView.transform;
                    newObj.transform.position = for_BottomScrollView.transform.position;
                    newObj.transform.localScale = new Vector2(1, 1);
                }
                break;
            case "Aqua":
                if (aqua_topScrollView.transform.childCount < 8)
                {
                    newObj.transform.parent = aqua_topScrollView.transform;
                    newObj.transform.position = aqua_topScrollView.transform.position;
                    newObj.transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    newObj.transform.parent = aqua_BottomScrollView.transform;
                    newObj.transform.position = aqua_BottomScrollView.transform.position;
                    newObj.transform.localScale = new Vector2(1, 1);
                }
                break;
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
        string selectedTheme = PlayerPrefs.GetString("Theme");
        switch(selectedTheme)
        {
            case "Default":
                //discovery_element.transform.position = def_discoveryTray_Panel.transform.position;
                discovery_element.transform.parent = def_discoveryTray_Panel.transform;
                setScaleOfDiscoveryElements();
                break;
            case "Forest":
                //discovery_element.transform.position = for_discoveryTray_Panel.transform.position;
                discovery_element.transform.parent = for_discoveryTray_Panel.transform;
                setScaleOfDiscoveryElements();
                break;
            case "Aqua":
                //discovery_element.transform.position = aqua_discoveryTray_Panel.transform.position;
                discovery_element.transform.parent = aqua_discoveryTray_Panel.transform;
                setScaleOfDiscoveryElements();
                break;
        }
        
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

    /*
    public void LoadDisabledGameObjectsList()
    {
        if (PlayerPrefs.HasKey("DisabledCollidedGameObject"))
        {
            string saveDisObj = PlayerPrefs.GetString("DisabledCollidedGameObject");
            FindObjectOfType<combinationManager>().disabledGameobjects.AddRange(saveDisObj.Split(';'));
        }
    }
    */
    
    
    public void CleanUpTable()
    {
        foreach (GameObject g in tempNewCreatedObj)
        {
            g.gameObject.SetActive(false);
            PlayerPrefs.SetInt("CleanedUpNewCreatedElement", 1);
            if(!disabledGameobjects.Contains(g.name))
            {
                disabledGameobjects.Add(g.name);
            }
            elementManager.saveDisabledGameObjectsList();
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

    public void GiveWelcomeMessage()
    {
        WelcomeMessage = charMessages.ReturnWelcomingMessages();
        charManager.HandlingCharacterBehaviour(WelcomeMessage,20);
        soundManager.PlayCharacterSoundForIntro();
    }

    public void GiveNewElementFoundMessage()
    {
        NewElementFoundMessage = charMessages.ReturnNewElementFoundMessages();
        charManager.HandlingCharacterBehaviour(NewElementFoundMessage,20);
    }

    public void GiveNoCombinationFoundMessage()
    {
        NoCombinationExistsMessage = charMessages.ReturnNoCombinationExistsMessages();
        charManager.HandlingCharacterBehaviour(NoCombinationExistsMessage,20);
    }
    public void GiveCombinationAlreadyExistsMessage()
    {
        CombinationExistsMessage = charMessages.ReturnNoCombinationExistsMessages();
        charManager.HandlingCharacterBehaviour(CombinationExistsMessage,20);
    }
    public void playSoundFromSoundManager()
    {
        soundManager.PlayArrowButtonSound();
    }
}