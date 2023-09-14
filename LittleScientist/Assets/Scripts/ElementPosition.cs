using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPosition : MonoBehaviour
{
    public Vector2 InitialPos;
    public Vector3 FinalPos;

    public void GetInitialPos(Vector2 initialPos)
    {
        InitialPos = initialPos;
    }
    public void GetFinalPos(Vector2 finalPos)
    {
        FinalPos = finalPos;
    }
}
