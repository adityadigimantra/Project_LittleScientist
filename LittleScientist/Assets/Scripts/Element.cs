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

    [Header("Element Colliding Data")]
    public string Element1;
    public string Element2;

    private void Start()
    {
        obj = gameObject;
        obj.name = newElementName;
        //obj.GetComponent<Image>().sprite = newSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;
        string thisObjectName = gameObject.name;
        string otherObjectName = otherObj.name;
        //Setting names to Public Fields
        Element1 = thisObjectName;
        Element2 = otherObjectName;
        Debug.Log("element 1" + thisObjectName + "Collided with Element 2" + otherObjectName);
    }

}
