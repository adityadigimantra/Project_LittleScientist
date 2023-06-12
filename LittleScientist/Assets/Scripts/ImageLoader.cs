using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public Image[] elementImages;
    public ImageLoader imageLoader;
    public combinationManager com;
    public GameObject tempObj;
    public void Start()
    {
        imageLoader = GetComponent<ImageLoader>();
        com = FindObjectOfType<combinationManager>();
    }

    public void Update()
    {
        tempObj = com.newObj;
        
    }
}
