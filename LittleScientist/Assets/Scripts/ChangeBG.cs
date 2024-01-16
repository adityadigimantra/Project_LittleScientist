using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBG : MonoBehaviour
{

    public Sprite[] bgImages;
    public Image component;
    // Start is called before the first frame update
    public void ChangeBackGround1()
    {
        component.GetComponent<Image>().sprite = bgImages[0];
    }
    public void ChangeBackGround2()
    {
        component.GetComponent<Image>().sprite = bgImages[1];
    }
    public void ChangeBackGround3()
    {
        component.GetComponent<Image>().sprite = bgImages[2];
    }
}
