using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Element Data")]
    public string currentElementName;
    public GameObject thisObject;
    public  CanvasGroup canvasGroup;
    public  RectTransform rectTransform;
    public GameObject copiedGameObject;
    public GameObject newGameObject;

    public GameObject ElementsPanel;




    private void Start()
    {
        ElementsPanel = GameObject.Find("ElementsPanel");
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        thisObject = this.gameObject;
        
    }
    private void Update()
    {
        /*
        if(gameObject.tag=="Original"&&copiedGameObject!=null)
        {
            newGameObject = copiedGameObject;
            parentGameObject = GameObject.FindGameObjectWithTag("Original");
            copiedGameObject.name = this.gameObject.name;
        }
        */
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
        copiedGameObject = Instantiate(prefab);
        copiedGameObject.transform.parent = ElementsPanel.transform;
        copiedGameObject.gameObject.GetComponent<CanvasGroup>().alpha = 0.6f;
        copiedGameObject.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        copiedGameObject.gameObject.GetComponent<BoxCollider2D>().enabled =false;
        copiedGameObject.gameObject.name = gameObject.GetComponent<Element>().elementName;
        copiedGameObject.gameObject.tag = "Copied";
    }

    public void OnDrag(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition += eventData.delta / GetCanvasScale();
        //copiedGameObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta/GetCanvasScale();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // Reset the transparency of the image when dragging ends
        canvasGroup.blocksRaycasts = true;
        //copiedGameObject.GetComponent<CanvasGroup>().alpha = 1f;
        //copiedGameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        //copiedGameObject.gameObject.GetComponent<BoxCollider2D>().enabled = true;
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
