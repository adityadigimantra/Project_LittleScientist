using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingElementsReset : MonoBehaviour
{
    public BoxCollider2D boxCollider2d;
    public string CollidedObjName;
    public GameObject CollidedObj;
    private void Start()
    {
        boxCollider2d = this.gameObject.GetComponent<BoxCollider2D>();
    }
    public void CleanUpSpace()
    {
        PlayerPrefs.SetInt("DestroyAll", 1);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
