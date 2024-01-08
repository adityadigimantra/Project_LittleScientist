using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectElements : MonoBehaviour
{
    public GameObject ScrollRect_Content;
    public GameObject InsideRingElement;

    [Header("Instances")]
    public combinationManager comManager;
    public ElementManager elementManager;
    private void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        elementManager = FindObjectOfType<ElementManager>();
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (ScrollRect_Content.transform.childCount < 8)
        {
            if (other.GetComponent<CheckStatus>().isInsidePlayArea)
            {
                other.gameObject.GetComponent<CheckStatus>().thisObjectAnimator.SetBool("IsOpen", false);
                if (!comManager.disabledGameobjects.Contains(other.name))
                {
                    comManager.disabledGameobjects.Add(other.name);
                }
                elementManager.saveDisabledGameObjectsList();
                StartCoroutine(givingDelaythenDestroy(other));
                //Destroy(other.gameObject);
                //Transform existingChild = ScrollRect_Content.transform.Find(other.name);
                //
                //if(existingChild==null)
                //{
                //    //Removed feature-To arrange elements to either scroll rects.
                //
                //   
                //}
                //else
                //{
                //    Destroy(other.gameObject);
                //}

            }

        }
        else
        {
            //If Limit exceeds
            if (other.GetComponent<CheckStatus>().isInsidePlayArea)
            {
                other.gameObject.GetComponent<CheckStatus>().thisObjectAnimator.SetBool("IsOpen", false);
                if (!comManager.disabledGameobjects.Contains(other.name))
                {
                    comManager.disabledGameobjects.Add(other.name);
                }
                elementManager.saveDisabledGameObjectsList();
                StartCoroutine(givingDelaythenDestroy(other));
                //Destroy(other.gameObject);
            }
        }
    }

    IEnumerator givingDelaythenDestroy(Collider2D obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj.gameObject);
    }
}

