using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{

    [Header("Instance")]
    public combinationManager comManager;
    public ElementManager elementManager;
    public SoundManager soundManager;

    private void Start()
    {
        comManager = FindObjectOfType<combinationManager>();
        elementManager = FindObjectOfType<ElementManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //If it is Inside Play area and user then drags elements to Trash.

        if(other.GetComponent<CheckStatus>().isInsidePlayArea)
        {
            other.gameObject.GetComponent<CheckStatus>().thisObjectAnimator.SetBool("IsOpen", false);
            if(!comManager.elementsDraggedToTrash.Contains(other.name))
            {
                comManager.elementsDraggedToTrash.Add(other.name);
            }
            elementManager.SaveTrashGameObjectsList();
            soundManager.PlayTrashSound();
            StartCoroutine(givingDelaythenDestroy(other));
        }
        //If user directly drags element to Trash.
        else
        {
            other.gameObject.GetComponent<CheckStatus>().thisObjectAnimator.SetBool("IsOpen", false);
            if (!comManager.elementsDraggedToTrash.Contains(other.name))
            {
                comManager.elementsDraggedToTrash.Add(other.name);
            }
            elementManager.SaveTrashGameObjectsList();
            soundManager.PlayTrashSound();
            StartCoroutine(givingDelaythenDestroy(other));
        }
    }
    IEnumerator givingDelaythenDestroy(Collider2D obj)
    {
        yield return new WaitForSeconds(1);
        obj.gameObject.SetActive(false);
    }
}
