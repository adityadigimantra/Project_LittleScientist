using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    public string elementName;
    public string newElementName;
    public Sprite newSprite;
    public GameObject obj;
    private void Start()
    {
        obj = gameObject;
        obj.name = newElementName;
        obj.GetComponent<Image>().sprite = newSprite;
    }
}
