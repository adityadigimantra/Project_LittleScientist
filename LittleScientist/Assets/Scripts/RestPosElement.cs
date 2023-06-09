using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestPosElement : MonoBehaviour
{
    public bool isBackPos=false; 
    public Vector2 fixPos;
    public DragAndDrop dragAnddrop;

    private void Start()
    {
        fixPos = this.gameObject.transform.position;
        //dragAnddrop = FindObjectOfType<DragAndDrop>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;
        otherObj.transform.position = fixPos;
        //Destroy(dragAnddrop.currentCopy);
    }
}
