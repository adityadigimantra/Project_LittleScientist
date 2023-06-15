using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    public string elementName;
     GameObject obj;
    combinationManager comManager;
    [Header("Element Colliding Data")]
    public string ELE_Element1;
    public string ELE_Element2;
    public Vector3 Element1Pos;
    public Vector3 Element2Pos;
    public Vector3 averagePos;

    private void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        obj = this.gameObject;
        //obj.GetComponent<Image>().sprite = newSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SoundManager._instance.elementCollideSound();
        GameObject otherObj = other.gameObject;
        string thisObjectName = gameObject.name;
        Element1Pos = gameObject.transform.position;
        Element2Pos = other.transform.position;
        string otherObjectName = otherObj.name;
        comManager.creatingNewElement = true;
        ELE_Element1 = gameObject.name;
        PlayerPrefs.SetString("element1", ELE_Element1);
        ELE_Element2 = otherObjectName;
        PlayerPrefs.SetString("element2", ELE_Element2);
        Debug.Log("element 1" + thisObjectName + "Collided with Element 2" + otherObjectName);
        averagePos=(Element1Pos + Element2Pos) / 2f;
        //StartCoroutine(destroyingObj(this.gameObject,otherObj));
    }
    IEnumerator destroyingObj(GameObject obj1,GameObject obj2)
    {
        if(comManager.combinationFound)
        {
            GameObject ele1 = obj1;
            GameObject ele2 = obj2;
            yield return new WaitForSeconds(1f);
            Destroy(ele1);
            Destroy(ele2);
        }
    }

}
