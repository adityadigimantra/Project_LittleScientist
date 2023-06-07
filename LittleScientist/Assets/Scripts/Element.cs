using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    public string elementName;
     GameObject obj;

    [Header("Element Colliding Data")]
    public string ELE_Element1;
    public string ELE_Element2;
    public string[] two_Elements=new string[2];

    private void Start()
    {
        obj = this.gameObject;
        elementName = obj.name;
        //obj.GetComponent<Image>().sprite = newSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;

        //Setting names to Public Fields
        for(int i=0;i<two_Elements.Length;i++)
        {
            string thisObjectName = gameObject.name;
            string otherObjectName = otherObj.name;
            ELE_Element1 = gameObject.name;
            ELE_Element2 = otherObjectName;
            Debug.Log("element 1" + thisObjectName + "Collided with Element 2" + otherObjectName);
        }
       
    }

}
