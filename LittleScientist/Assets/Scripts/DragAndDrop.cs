using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    //This Script creates a new GameObject called Copied Gameobject and also helps to drag that copied
    //gameobject in the scene
    [Header("Instances")]
    public SoundManager soundManager;
    public TutorialManager tutorialManager;
    public GameOperation gameOperation;
    public ThemeManager themeManager;

   [Header("Element Data")]
    public string currentElementName;
    public GameObject thisObject;
    public  CanvasGroup canvasGroup;
    public  RectTransform rectTransform;
    public GameObject copiedGameObject;
    public GameObject newGameObject;
   
    public RectTransform ClampPanelRectTransform;
    public Text ElementNameTextObj;

    [Header("Theme Elements Panels")]
    public GameObject def_ElementsPanel;
    public GameObject for_ElementsPanel;
    public GameObject aqua_ElementsPanel;

    [Header("Clamping Movement Data")]
    private Vector2 initialPosition;
    private RectTransform outerPanelTransform;


    [Header("Top and Bottom Scroll Rects And Trash")]
    public GameObject def_topScrollRect;
    public GameObject def_bottomScrollRect;
    public GameObject def_Trash;
    public GameObject for_topScrollRect;
    public GameObject for_bottomScrollRect;
    public GameObject for_Trash;
    public GameObject aqua_topScrollRect;
    public GameObject aqua_bottomScrollRect;
    public GameObject aqua_Trash;

    public string selectedTheme;
    private void Start()
    {
        Debug.Log("Hello From DragNDrop");
        soundManager = FindObjectOfType<SoundManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
        themeManager = FindObjectOfType<ThemeManager>();
        gameOperation = FindObjectOfType<GameOperation>();
        themeManager.SettingIfNoThemeSelected();

        selectedTheme = PlayerPrefs.GetString("Theme");
        //ClampPanelRectTransform = GameObject.Find("ClampPanel").GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        ClampPanelRectTransform = GameObject.Find("Boundary").GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if(this.gameObject.tag=="InsideRingElement")
        {
            thisObject = this.gameObject;
            
        }
        ElementNameTextObj.text = ConvertToUpperCase(this.gameObject.name);


    }
    private void Update()
    {
        if(!string.IsNullOrEmpty(selectedTheme))
        {
            switch (selectedTheme)
            {
                case "Default":
                    if (gameOperation.DefaultMainObj.activeSelf)
                    {
                        def_topScrollRect = GameObject.Find("Def_Top_ScrollView");
                        def_bottomScrollRect = GameObject.Find("Def_Down_ScrollView");
                        def_ElementsPanel = GameObject.Find("def_AllElements");
                        def_Trash = GameObject.Find("def_Trash");
                    }
                    break;

                case "Forest":
                    if (gameOperation.ForestMainObj.activeSelf)
                    {
                        for_topScrollRect = GameObject.Find("for_Top_ScrollView");
                        for_bottomScrollRect = GameObject.Find("for_Down_ScrollView");
                        for_ElementsPanel = GameObject.Find("for_AllElements");
                        for_Trash = GameObject.Find("for_Trash");
                    }
                    break;
                case "Aqua":
                    if (gameOperation.AquaMainObj.activeSelf)
                    {
                        aqua_topScrollRect = GameObject.Find("aqua_Top_ScrollView");
                        aqua_bottomScrollRect = GameObject.Find("aqua_Down_ScrollView");
                        aqua_ElementsPanel = GameObject.Find("aqua_AllElements");
                        aqua_Trash = GameObject.Find("aqua_Trash");
                    }
                    break;
            }
        }
        
    }
    public void OnClick_SwitchOff_TopAndBottomScrollRectsAndTrash()
    {
        switch (selectedTheme)
        {
            case "Default":
                def_topScrollRect.GetComponent<BoxCollider2D>().enabled = false;
                def_bottomScrollRect.GetComponent<BoxCollider2D>().enabled = false;
                def_Trash.GetComponent<BoxCollider2D>().enabled = false;
                break;

            case "Forest":
                for_topScrollRect.GetComponent<BoxCollider2D>().enabled = false;
                for_bottomScrollRect.GetComponent<BoxCollider2D>().enabled = false;
                for_Trash.GetComponent<BoxCollider2D>().enabled = false;
                break;
            case "Aqua":
                aqua_topScrollRect.GetComponent<BoxCollider2D>().enabled = false;
                aqua_bottomScrollRect.GetComponent<BoxCollider2D>().enabled = false;
                aqua_Trash.GetComponent<BoxCollider2D>().enabled = false;
                break;
        }
        
       
    }
    public void OnClick_SwitchOn_TopAndBottomScrollRectsAndTrash()
    {
        switch(selectedTheme)
        {
            case "Default":
                def_topScrollRect.GetComponent<BoxCollider2D>().enabled = true;
                def_bottomScrollRect.GetComponent<BoxCollider2D>().enabled = true;
                def_Trash.GetComponent<BoxCollider2D>().enabled = true;
                break;

            case "Forest":
                for_topScrollRect.GetComponent<BoxCollider2D>().enabled = true;
                for_bottomScrollRect.GetComponent<BoxCollider2D>().enabled = true;
                for_Trash.GetComponent<BoxCollider2D>().enabled = true;
                break;
            case "Aqua":
                aqua_topScrollRect.GetComponent<BoxCollider2D>().enabled = true;
                aqua_bottomScrollRect.GetComponent<BoxCollider2D>().enabled = true;
                aqua_Trash.GetComponent<BoxCollider2D>().enabled = true;
                break;
        }
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {

        if (thisObject.tag == "InsideRingElement")
        {
            canvasGroup.alpha = 0.6f; // Adjust the transparency of the image when dragging
            canvasGroup.blocksRaycasts = false;
            copiedElementData();
            soundManager.PlayGeneralButtonTapSound();
            tutorialManager.FirstHand.GetComponent<Animator>().SetBool("CloseHand1", true);
            OnClick_SwitchOff_TopAndBottomScrollRectsAndTrash();
            //tutorialManager.FirstHand.SetActive(false);
        }
    }

    public void copiedElementData()
    {

        GameObject prefab = FindObjectOfType<combinationManager>().Copied;
        copiedGameObject = Instantiate(prefab,thisObject.transform.position,Quaternion.identity);
        copiedGameObject.transform.localScale = new Vector3(1f,1f,1f);
        switch(selectedTheme)
        {
            case "Default":
                copiedGameObject.transform.parent = def_ElementsPanel.transform;
                copiedGameObject.transform.SetAsLastSibling();
                break;
            case "Forest":
                copiedGameObject.transform.parent = for_ElementsPanel.transform;
                copiedGameObject.transform.SetAsLastSibling();
                break;
            case "Aqua":
                copiedGameObject.transform.parent = aqua_ElementsPanel.transform;
                copiedGameObject.transform.SetAsLastSibling();
                break;
        }
       
        
        copiedGameObject.GetComponent<CanvasGroup>().alpha = 0.6f;
        copiedGameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        copiedGameObject.GetComponent<BoxCollider2D>().enabled =false;
        copiedGameObject.gameObject.name = gameObject.GetComponent<Element>().thisElementName;
        copiedGameObject.gameObject.tag = "Copied";
        copiedGameObject.transform.SetAsLastSibling();
        PlayerPrefs.SetString("UpperObject", copiedGameObject.gameObject.name);
        //Debug.Log("Upper GameObject Name" + PlayerPrefs.GetString("UpperObject"));
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = eventData.position;
       Vector2 localPosition;
        
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(ClampPanelRectTransform,mousePosition,eventData.pressEventCamera,out localPosition))
        {
            if(copiedGameObject!=null)
            {
                localPosition.x = Mathf.Clamp(localPosition.x, ClampPanelRectTransform.rect.min.x + copiedGameObject.GetComponent<RectTransform>().rect.width, ClampPanelRectTransform.rect.max.x - copiedGameObject.GetComponent<RectTransform>().rect.width);
                localPosition.y = Mathf.Clamp(localPosition.y, ClampPanelRectTransform.rect.min.y + copiedGameObject.GetComponent<RectTransform>().rect.height, ClampPanelRectTransform.rect.max.y - copiedGameObject.GetComponent<RectTransform>().rect.height);
                copiedGameObject.GetComponent<RectTransform>().anchoredPosition = localPosition;
                copiedGameObject.GetComponent<BoxCollider2D>().enabled = true;
                //Uncomment Below Line if Neccessary
                copiedGameObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta / GetCanvasScale();
            }
            
        }
       
    }

    private Vector2 clampPosition(Vector2 position)
    {
        Vector3[] corners = new Vector3[4];
        outerPanelTransform.GetWorldCorners(corners);

        float minX = corners[0].x;
        float maxX = corners[2].x - thisObject.GetComponent<RectTransform>().rect.width;
        float minY = corners[0].y;
        float maxY = corners[1].y - thisObject.GetComponent<RectTransform>().rect.height;

        float clampedX = Mathf.Clamp(position.x, minX, maxX);
        float clampedY = Mathf.Clamp(position.y, minY, maxY);

        return new Vector2(clampedX, clampedY);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (copiedGameObject != null)
        {
            //For This Object
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            //For Copied Object
            copiedGameObject.GetComponent<Copied_DragNDrop>().StartCombinationProcess();
            copiedGameObject.GetComponent<CanvasGroup>().alpha = 1f;
            copiedGameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            copiedGameObject.GetComponent<BoxCollider2D>().enabled = true;
            //if (!copiedGameObject.GetComponent<CheckStatus>().isInsidePlayArea)
            //{
            //    Destroy(copiedGameObject);
            //}
        }
        else
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
        OnClick_SwitchOn_TopAndBottomScrollRectsAndTrash();
    }

    private float GetCanvasScale()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
            return canvas.scaleFactor;
        else
            return 1f;
    }

    public string ConvertToUpperCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;

        }
        else
        {
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
