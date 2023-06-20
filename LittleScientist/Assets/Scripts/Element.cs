using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    [Header("Element Data")]
    public string elementName;

    combinationManager comManager;

    [Header("Element Colliding Data")]
    public string ELE_Element1Name;
    public string ELE_Element2Name;
    public GameObject ELE_Element1Obj;
    public GameObject ELE_Element2Obj;
    public Vector2 ELE_Element1Pos;
    public Vector2 ELE_Element2Pos;
    public Vector2 averagePos;
    

    private void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        elementName = this.gameObject.name;
        ELE_Element1Obj = this.gameObject;
    }

    private void Update()
    {
        //Getting live Position of Both Element
        getPositionOfElements();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //SoundManager._instance.elementCollideSound();
        ELE_Element2Obj = other.gameObject;
        ELE_Element1Name = ELE_Element1Obj.name;
        PlayerPrefs.SetString("element1", ELE_Element1Name);
        ELE_Element2Name = ELE_Element2Obj.name;
        PlayerPrefs.SetString("element2", ELE_Element2Name);
        Debug.Log("Element 1=" + ELE_Element1Obj + "Collided with Element 2=" + ELE_Element2Obj);  
    }

    public void getPositionOfElements()
    {
        ELE_Element1Pos = ELE_Element1Obj.GetComponent<RectTransform>().transform.position;
        if(ELE_Element2Obj!=null)
        {
            ELE_Element2Pos = ELE_Element2Obj.GetComponent<RectTransform>().transform.position;
            averagePos = (ELE_Element1Pos + ELE_Element2Pos) / 2;
        }
        
    }

}
