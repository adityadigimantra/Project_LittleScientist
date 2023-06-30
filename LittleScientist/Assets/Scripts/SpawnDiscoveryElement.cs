using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDiscoveryElement : MonoBehaviour
{

    private void Start()
    {
    }

    private void Update()
    {
        
    }

    public void setDiscoveryElementInsideRect()
    {
        PlayerPrefs.SetInt("InsideRectangle",1);
        PlayerPrefs.SetInt("InsidePlayArea", 0);
    }
}
