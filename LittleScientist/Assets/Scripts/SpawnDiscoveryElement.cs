using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDiscoveryElement : MonoBehaviour
{
    [Header("Element Data")]
    public string elementName;

    private void Start()
    {
        elementName = this.gameObject.name;
    }

    private void Update()
    {
        
    }

    public void selectDiscoveryElement()
    {
        PlayerPrefs.SetString("DiscoveryElementSelected", elementName);
    }
}
