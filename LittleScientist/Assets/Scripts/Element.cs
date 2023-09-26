using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
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
    public bool isCollided = false;
    public string loadedString;
    public Vector2 loadedVector;
    public Vector2 newelementCreatedPos;


    [Header("Instances")]
    public combinationManager comManager;



    private void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        thisElementName = this.gameObject.name;
        thisElementObj = this.gameObject;
        if(this.gameObject.tag=="Copied")
        {
            thisElementImage = this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite;
        }
        else
        {
            thisElementImage = gameObject.transform.GetChild(1).GetComponent<Image>().sprite;
        }
        
    }

    private void Update()
    {
        loadedString = PlayerPrefs.GetString("CreatedElementData");
    }

    public  void OnTriggerEnter2D(Collider2D other)
    {

            string[] excludedObjects = new string[] { "ScrollRect", "PlayArea" };
            if (Array.IndexOf(excludedObjects, other.gameObject.tag) == -1)
            {
                Debug.Log("OnTrigger Enter Called");
                GameOperation._Instance.GameState = GameState.Playing;
                PlayerPrefs.SetInt("IsRestart", 0);
                SoundManager._instance.elementCollideSound();
                otherElementObj = other.gameObject;
                OtherElementName = otherElementObj.name;
                thisElementName = thisElementObj.name;
                isCollided = true;
                if(gameObject.tag=="Copied")
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
                else if(gameObject.tag=="NewCreatedElement")
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                gameObject.GetComponent<Copied_DragNDrop>().GetAnotherGameObject(otherElementObj);
                getPositionOfElements();
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        string[] excludedObjects = new string[] { "ScrollRect", "PlayArea" };
        if (Array.IndexOf(excludedObjects, other.gameObject.tag) == -1)
        {
            Debug.Log("OnTriggerExit Called ");
            isCollided = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
   
    public void getPositionOfElements()
    {
        thisElementPosition = thisElementObj.GetComponent<RectTransform>().transform.position;
        if (otherElementObj != null)
        {
            otherElementPosition = otherElementObj.GetComponent<RectTransform>().transform.position;
            averagePos = (thisElementPosition + otherElementPosition) / 2;
        }
        string vectorString = convertVectorToString(averagePos);
        PlayerPrefs.SetString(loadedString, vectorString);
        string savedVectorString = PlayerPrefs.GetString(loadedString);
        loadedVector = converStringToVector(savedVectorString);


    }

    public string convertVectorToString(Vector2 vector)
    {
        return vector.x.ToString() + "," + vector.y.ToString();
    }
    public Vector2 converStringToVector(string vectorString)
    {
        float x = 0f;
        float y = 0f;
        string[] components = vectorString.Split(',');
        bool parseSuccess = false;
        if (components.Length >= 2)
        {
            parseSuccess = float.TryParse(components[0], out x) && float.TryParse(components[1], out y);

        }
        if (parseSuccess)
        {
            Vector2 vector = new Vector2(x, y);
            return vector;
        }
        else
        {
            Debug.LogError("Failed to parse Vector2 from string: " + vectorString);
            return Vector2.zero;

        }


    }

   
    

}