using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleAnimation : MonoBehaviour
{
    public Transform ObjectToShake;
    public float shakeDuration = 0.5f;
    public float shakeIntensity = 0.1f;

    private Vector3 originalPosition;
    private float shakeEndTime=0f;


    private void Update()
    {
        if(Time.time<shakeEndTime)
        {
            Vector2 shakeOffset = Random.insideUnitCircle * shakeIntensity;
            ObjectToShake.position = originalPosition + new Vector3(shakeOffset.x, shakeOffset.y, 0);
        }
    }

    public void startShake()
    {
        originalPosition = ObjectToShake.position;
        shakeEndTime = Time.time + shakeDuration;
    }
}
