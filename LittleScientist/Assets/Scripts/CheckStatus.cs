using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStatus : MonoBehaviour
{
    public bool isInsidePlayArea = false;
    public Animator thisObjectAnimator;

    private void Start()
    {
        thisObjectAnimator = gameObject.GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayArea")
        {
            isInsidePlayArea = true;
        }
        if(other.gameObject.tag=="ScrollRect")
        {
            //isInsidePlayArea = false;
        }
    }
   
}
