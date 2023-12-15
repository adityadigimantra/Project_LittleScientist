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
                Transform existingChild = ScrollRect_Content.transform.Find(other.name);

                if(existingChild==null)
                {
                    //Removed feature-To arrange elements to either scroll rects.

                    GameObject newObj = Instantiate(InsideRingElement);
                    newObj.name = other.name;
                    newObj.transform.GetChild(1).GetComponent<Image>().sprite = other.gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite;
                    newObj.GetComponent<BoxCollider2D>().enabled = false;
                    newObj.transform.localScale = new Vector2(1f, 1f);
                    newObj.transform.position = ScrollRect_Content.transform.position;
                    newObj.transform.parent = ScrollRect_Content.transform;
                    Destroy(other.gameObject);
                }
                else
                {
                    Destroy(other.gameObject);
                }

            }

        }
        else
        {
            //If Limit exceeds
           if(other.GetComponent<CheckStatus>().isInsidePlayArea)
            {
                Destroy(other.gameObject);
            }
        }
    }

}
