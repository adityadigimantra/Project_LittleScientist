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
    public GameObject[] SameNameTagObj;
    public bool isColliding = false;
    public bool isCollided = false;


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
        SameNameTagObj = GameObject.FindGameObjectsWithTag("Copied");
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
        //Debug.Log("Element 1=" + thisElementObj + "Collided with Element 2=" + otherElementObj);
        //isCollided = true;
        //PlayerPrefs.SetInt("GameStarted", 1);
        comManager.handleCombination(thisElementName, OtherElementName);
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
            //To Search all the Elements with same Name Present in the Scene
            foreach(GameObject g in SameNameTagObj)
            {
                Element element = g.GetComponent<Element>();
                if(element!=null && element.isCollided)
                {
                    //Debug.Log("GameObject " + g.name + "has collided");
                    g.SetActive(false);
                }    
            }
            PlayerPrefs.SetInt("elementCreated", 0);
        }

    }

    public void savePositionofElements(Vector3 position,string key)
    {
        string posString = position.x.ToString() + "," + position.y.ToString() + "," + position.z.ToString();
        PlayerPrefs.SetString(key, posString);
    }
    

}
