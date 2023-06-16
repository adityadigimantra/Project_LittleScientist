using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Copied_DragNDrop : MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler
{
    [Header("Element Data")]
    public string currentElementName;
    public GameObject thisObject;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private void Start()
    {
        currentElementName = gameObject.name;
        thisObject = this.gameObject;
        Debug.Log("ElementName=" + currentElementName);
        rectTransform = thisObject.GetComponent<RectTransform>();
        canvasGroup = thisObject.GetComponent<CanvasGroup>();
        LoadSetImageforthisElement();
    }
    private void Update()
    {
        
    }

    public void LoadSetImageforthisElement()
    {
        string imagePath = "Elements/" + currentElementName;
        Sprite image = Resources.Load<Sprite>(imagePath);
        gameObject.GetComponent<Image>().sprite = image;
        gameObject.GetComponent<Image>().preserveAspect = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        thisObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        thisObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
       rectTransform.anchoredPosition += eventData.delta / GetCanvasScale();
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

