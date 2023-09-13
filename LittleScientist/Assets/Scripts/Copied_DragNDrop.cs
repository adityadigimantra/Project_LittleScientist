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
    public RectTransform ClampPanelRectTransform;
    public bool isDragging = false;
    private void Start()
    {
        currentElementName = gameObject.name;
        thisObject = this.gameObject;
        rectTransform = thisObject.GetComponent<RectTransform>();
        canvasGroup = thisObject.GetComponent<CanvasGroup>();
        LoadSetImageforthisElement();
        ClampPanelRectTransform = GameObject.Find("Boundary").GetComponent<RectTransform>();

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
        isDragging = true;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = eventData.position;
        Vector2 localPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(ClampPanelRectTransform, mousePosition, eventData.pressEventCamera, out localPosition))
        {
            localPosition.x = Mathf.Clamp(localPosition.x, ClampPanelRectTransform.rect.min.x + thisObject.GetComponent<RectTransform>().rect.width, ClampPanelRectTransform.rect.max.x - thisObject.GetComponent<RectTransform>().rect.width);
            localPosition.y = Mathf.Clamp(localPosition.y, ClampPanelRectTransform.rect.min.y + thisObject.GetComponent<RectTransform>().rect.height, ClampPanelRectTransform.rect.max.y - thisObject.GetComponent<RectTransform>().rect.height);
            thisObject.GetComponent<RectTransform>().anchoredPosition = localPosition;
        }
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

