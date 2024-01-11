using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{

    [Header("Instance")]
    public combinationManager comManager;
    public ElementManager elementManager;

    private void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        elementManager = FindObjectOfType<ElementManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<CheckStatus>().isInsidePlayArea)
        {
            other.gameObject.GetComponent<CheckStatus>().thisObjectAnimator.SetBool("IsOpen", false);
            if(!comManager.disabledGameobjects.Contains(other.name))
            {
                comManager.disabledGameobjects.Add(other.name);
            }
            elementManager.saveDisabledGameObjectsList();
            StartCoroutine(givingDelaythenDestroy(other));
        }
    }
    IEnumerator givingDelaythenDestroy(Collider2D obj)
    {
        yield return new WaitForSeconds(1);
        //Destroy(obj.gameObject);
    }
}
