using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectElements : MonoBehaviour
{
    public GameObject ScrollRect_Content;
    public GameObject InsideRingElement;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(ScrollRect_Content.transform.childCount<8)
        {
            if(other.GetComponent<CheckStatus>().isInsidePlayArea)
            {
                GameObject newObj = Instantiate(InsideRingElement);
                newObj.name = other.name;
                newObj.GetComponent<Image>().sprite = other.GetComponent<Image>().sprite;
                newObj.GetComponent<BoxCollider2D>().enabled = false;
                newObj.transform.localScale = new Vector2(1f, 1f);
                newObj.transform.position = ScrollRect_Content.transform.position;
                newObj.transform.parent = ScrollRect_Content.transform;
                Destroy(other.gameObject);
            }

        }
        else
        {
           if(other.GetComponent<CheckStatus>().isInsidePlayArea)
            {
                Destroy(other.gameObject);
            }
        }
    }

}
