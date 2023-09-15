using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRectElements : MonoBehaviour
{
    public GameObject ScrollRect_Content;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(ScrollRect_Content.transform.childCount<8)
        {
            if(other.GetComponent<CheckStatus>().isInsidePlayArea)
            {
                other.transform.position = ScrollRect_Content.transform.position;
                other.transform.parent = ScrollRect_Content.transform;
                other.GetComponent<CheckStatus>().isInsidePlayArea = false;
            }
           
        }
    }

}
