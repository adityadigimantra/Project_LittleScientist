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
    public GameObject[] SameNameTagObj2;
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
        thisElementImage = this.gameObject.GetComponent<Image>().sprite;
    }

    private void Update()
    {
        //Getting live Position of Both Element
        loadedString = PlayerPrefs.GetString("CreatedElementData");
        SameNameTagObj = GameObject.FindGameObjectsWithTag("Copied");
        SameNameTagObj2 = GameObject.FindGameObjectsWithTag("NewCreatedElement");
        //CheckforElementPresence(FindObjectOfType<combinationManager>().loadedString);
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
        FindObjectOfType<combinationManager>().HandleCombination(thisElementName, OtherElementName);
        getPositionOfElements();
        isCollided = true;
        StartCoroutine(offIsCollidedBool());
        PlayerPrefs.SetInt("IsRestart", 0);
        // CheckforElementPresence();
    }

    IEnumerator offIsCollidedBool()
    {
        yield return new WaitForSeconds(0.5f);
        isCollided = false;
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
        PlayerPrefs.Save();
        //Debug.Log("Vector saved="+ vectorString);
        string savedVectorString = PlayerPrefs.GetString(loadedString);
        loadedVector = converStringToVector(savedVectorString);
        // Debug.Log("Vector loaded" + loadedVector);


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

    public void DestroyingObj()
    {
        if (PlayerPrefs.GetInt("elementCreated") == 1)
        {
            //To Search all the Elements with same Name Present in the Scene
            foreach (GameObject g in SameNameTagObj)
            {
                Element element = g.GetComponent<Element>();
                if (element.isCollided)
                {
                    g.SetActive(false);
                    FindObjectOfType<combinationManager>().disabledGameobjects.Add(g.name);
                }
            }
            foreach (GameObject g in SameNameTagObj2)
            {
                Element element = g.GetComponent<Element>();
                if (element.isCollided)
                {
                    g.SetActive(false);
                    FindObjectOfType<combinationManager>().disabledGameobjects.Add(g.name);

                }
            }
            saveDisabledGameObjectsList();
            PlayerPrefs.SetInt("elementCreated", 0);
        }

    }
    
public void saveDisabledGameObjectsList()
    {
        string saveDisObj = string.Join(";", FindObjectOfType<combinationManager>().disabledGameobjects.ToArray());
        PlayerPrefs.SetString("DisabledCollidedGameObject", saveDisObj);
        PlayerPrefs.Save();
    }
    

}