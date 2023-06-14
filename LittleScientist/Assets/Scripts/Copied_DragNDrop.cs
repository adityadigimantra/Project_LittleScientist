using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Copied_DragNDrop : MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        string elementname = gameObject.name;
        Debug.Log("ElementName=" + elementname);
        string imagePath = "Elements/" + elementname;
        Sprite image=Resources.Load<Sprite>(imagePath);
        gameObject.GetComponent<Image>().sprite = image;
        gameObject.GetComponent<Image>().preserveAspect = true;
    }
    private void Update()
    {
        
    }

    public Sprite LoadElementImage(string elemenName)
    {
        string imagePath = "Elements/" + elemenName;
        return Resources.Load<Sprite>(imagePath);
        Debug.Log("Image Loaded");
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
        gameObject.transform.position = eventData.position;
    }

}

