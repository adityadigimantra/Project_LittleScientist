using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TapAndDrop : MonoBehaviour,IPointerDownHandler
{
    [Header("Element Data")]
    public string currentElementName;
    public GameObject thisObject;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public RectTransform clampPanelRectTransform;


    [Header("Data from Combination Manager")]
    public GameObject addElementToRectPanel;
    private void Start()
    {
        currentElementName = this.gameObject.name;
        thisObject = this.gameObject;
        //Data from Combination Manager
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerPrefs.SetString("DiscoveryElementSelected",currentElementName);
        Debug.Log(PlayerPrefs.GetString("DiscoveryElementSelected"));
        FindObjectOfType<combinationManager>().SpawningDiscoveryElementInsideRect();

    }
}
