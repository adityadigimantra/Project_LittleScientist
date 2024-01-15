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

    public SoundManager soundManager;

    [Header("Element Data")]
    public string currentElementName;
    public GameObject thisObject;
    public  CanvasGroup canvasGroup;
    public  RectTransform rectTransform;
    public GameObject copiedGameObject;
    public GameObject newGameObject;
    public GameObject ElementsPanel;
    public RectTransform ClampPanelRectTransform;

    [Header("Clamping Movement Data")]
    private Vector2 initialPosition;
    private RectTransform outerPanelTransform;



    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        ElementsPanel = GameObject.Find("AllElements");
        //ClampPanelRectTransform = GameObject.Find("ClampPanel").GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        ClampPanelRectTransform = GameObject.Find("Boundary").GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if(this.gameObject.tag=="InsideRingElement")
        {
            thisObject = this.gameObject;
        }
        
    }
    private void Update()
    {
    }
    public void OnPointerDown(PointerEventData eventData)
    {

        if (thisObject.tag == "InsideRingElement")
        {
            canvasGroup.alpha = 0.6f; // Adjust the transparency of the image when dragging
            canvasGroup.blocksRaycasts = false;
            copiedElementData();
            soundManager.PlayGeneralButtonTapSound();
        }
    }

    public void copiedElementData()
    {

        GameObject prefab = FindObjectOfType<combinationManager>().Copied;
        copiedGameObject = Instantiate(prefab,thisObject.transform.position,Quaternion.identity);
        copiedGameObject.transform.localScale = new Vector3(1f,1f,1f);
        copiedGameObject.transform.parent = ElementsPanel.transform;
        copiedGameObject.transform.SetAsLastSibling();
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
        if(copiedGameObject!=null)
        {
            //For This Object
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            //For Copied Object
            copiedGameObject.GetComponent<Copied_DragNDrop>().StartCombinationProcess();
            copiedGameObject.GetComponent<CanvasGroup>().alpha = 1f;
            copiedGameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            copiedGameObject.GetComponent<BoxCollider2D>().enabled = true;
            if (!copiedGameObject.GetComponent<CheckStatus>().isInsidePlayArea)
            {
                Destroy(copiedGameObject);
            }
        }
        else
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        } 
    }

    private float GetCanvasScale()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
            return canvas.scaleFactor;
        else
            return 1f;
    }
}
