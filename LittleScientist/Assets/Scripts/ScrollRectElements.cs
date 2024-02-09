using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectElements : MonoBehaviour
{
    public GameObject ScrollRect_Content;
    public GameObject InsideRingElement;
    public Scrollbar bottomScrollbar;

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
        //Check if the count of Scroll Rect is less then 8
        if (ScrollRect_Content.transform.childCount < 8)
        {
            if (!other.GetComponent<CheckStatus>().isInsidePlayArea)
            {
                //Play sink Animation
                other.gameObject.GetComponent<CheckStatus>().thisObjectAnimator.SetBool("IsOpen", false);

                //Check if the element is already present in DisabledGameobject list
                if (!comManager.elementsDraggedToRects.Contains(other.name))
                {
                    comManager.elementsDraggedToRects.Add(other.name);
                }

                //Saving the list to file.
                elementManager.saveDisabledGameObjectsList();

                //Then destroying the element after playing animations.
                StartCoroutine(givingDelaythenDestroy(other));
            }

        }
        else
        {

            if (!other.GetComponent<CheckStatus>().isInsidePlayArea)
            {
                other.gameObject.GetComponent<CheckStatus>().thisObjectAnimator.SetBool("IsOpen", false);

                if (!comManager.disabledGameobjects.Contains(other.name))
                {
                    comManager.disabledGameobjects.Add(other.name);
                }

                elementManager.saveDisabledGameObjectsList();

                StartCoroutine(givingDelaythenDestroy(other));
                
            }
        }
    }

    IEnumerator givingDelaythenDestroy(Collider2D obj)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(obj.gameObject);
    }

    public void MoveToRight()
    {
        bottomScrollbar.GetComponent<Scrollbar>().value += 0.1f;
    }
    public void MoveToLeft()
    {
        bottomScrollbar.GetComponent<Scrollbar>().value -= 0.1f;
    }
}

