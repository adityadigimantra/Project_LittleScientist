using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    [Header("This Element Data")]
    public string thisElementName;
    public GameObject thisElementObj;
    public Vector2 thisElementPosition;
    public Sprite thisElementImage;

    [Header("Other Element Data")]
    public string OtherElementName;
    public GameObject otherElementObj;
    public Vector2 otherElementPosition;
    public Sprite otherElementImage;


    [Header("Element Colliding Data")]
    public GameObject[] tempObj;
    public Vector2 averagePos;
    public bool isColliding = false;

    [Header("Instances")]
    public combinationManager comManager;

    private void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        thisElementName = this.gameObject.name;
        thisElementObj = this.gameObject;
        thisElementImage = this.gameObject.GetComponent<Image>().sprite;
    }

    private void Update()
    {
        //Getting live Position of Both Element
        getPositionOfElements();
        DestroyingObj();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //SoundManager._instance.elementCollideSound();
        otherElementObj = other.gameObject;
        thisElementName = thisElementObj.name;
        PlayerPrefs.SetString("element1", thisElementName);
        OtherElementName = otherElementObj.name;
        PlayerPrefs.SetString("element2", OtherElementName);
        Debug.Log("Element 1=" + thisElementObj + "Collided with Element 2=" + otherElementObj);
    }

    public void getPositionOfElements()
    {
        thisElementPosition = thisElementObj.GetComponent<RectTransform>().transform.position;
        if(otherElementObj != null)
        {
            otherElementPosition = otherElementObj.GetComponent<RectTransform>().transform.position;
            averagePos = (thisElementPosition + otherElementPosition) / 2;
            savePositionofElements(averagePos,"averagePos");
        }
    }

    public void DestroyingObj()
    {
        if(PlayerPrefs.GetInt("elementCreated")==1)
        {
            string Obj1Name = PlayerPrefs.GetString("parentElement1");
            string Obj2Name = PlayerPrefs.GetString("parentElement2");
            tempObj = GameObject.FindGameObjectsWithTag("Copied");
            foreach(GameObject g in tempObj)
            {
                if(g.name==Obj1Name)
                {
                    g.SetActive(false);
                }
                if(g.name==Obj2Name)
                {
                    g.SetActive(false);
                }
            }
                PlayerPrefs.SetInt("elementCreated", 0);
                Debug.Log("Element Created Value" + PlayerPrefs.GetInt("elementCreated"));
           
        }

    }

    public void savePositionofElements(Vector3 position,string key)
    {
        string posString = position.x.ToString() + "," + position.y.ToString() + "," + position.z.ToString();
        PlayerPrefs.SetString(key, posString);
    }
    

}
