using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Copied_DragNDrop : MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler
{
    //This Object Handles the Tap and Drag of Copied Elements 

    [Header("Element Data")]
    public string currentElementName;
    public GameObject thisObject;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private void Start()
    {
        currentElementName = gameObject.name;
        thisObject = this.gameObject;
        rectTransform = thisObject.GetComponent<RectTransform>();
        canvasGroup = thisObject.GetComponent<CanvasGroup>();
        LoadSetImageforthisElement();

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
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
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

