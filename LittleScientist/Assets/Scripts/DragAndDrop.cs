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

    [Header("Element Data")]
    public string currentElementName;
    public GameObject thisObject;
    public  CanvasGroup canvasGroup;
    public  RectTransform rectTransform;
    public GameObject copiedGameObject;
    public GameObject newGameObject;
    public GameObject ElementsPanel;
    public RectTransform ClampPanelRectTransform;


    private void Start()
    {
        ElementsPanel = GameObject.Find("ElementsPanel");
        //ClampPanelRectTransform = GameObject.Find("ClampPanel").GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
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
        }
    }

    public void copiedElementData()
    {

        GameObject prefab = FindObjectOfType<combinationManager>().Copied;
        copiedGameObject = Instantiate(prefab,thisObject.transform.position,Quaternion.identity);
        copiedGameObject.transform.parent = ElementsPanel.transform;
        copiedGameObject.GetComponent<CanvasGroup>().alpha = 0.6f;
        copiedGameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        copiedGameObject.GetComponent<BoxCollider2D>().enabled =false;
        copiedGameObject.gameObject.name = gameObject.GetComponent<Element>().elementName;
        copiedGameObject.gameObject.tag = "Copied";
    }

    public void OnDrag(PointerEventData eventData)
    {
       //Vector2 mousePosition = eventData.position;
       // Vector2 localPosition;
        /*
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(ClampPanelRectTransform,mousePosition,eventData.pressEventCamera,out localPosition))
        {
           localPosition.x=Mathf.Clamp(localPosition.x, ClampPanelRectTransform.rect.min.x, ClampPanelRectTransform.rect.max.x - copiedGameObject.GetComponent<RectTransform>().rect.width);
           localPosition.y = Mathf.Clamp(localPosition.y, ClampPanelRectTransform.rect.min.y, ClampPanelRectTransform.rect.max.y - copiedGameObject.GetComponent<RectTransform>().rect.height);
           copiedGameObject.GetComponent<RectTransform>().anchoredPosition = localPosition;
        }
        */
        copiedGameObject.GetComponent<BoxCollider2D>().enabled = true;
        copiedGameObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta / GetCanvasScale();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //For This Object
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        //For Copied Object
        copiedGameObject.GetComponent<CanvasGroup>().alpha = 1f;
        copiedGameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        copiedGameObject.GetComponent<BoxCollider2D>().enabled = true;
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
