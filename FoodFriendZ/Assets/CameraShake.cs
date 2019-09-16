using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float maxShakeTime;
    private float shakeDuration = 0f;
    public float intensity = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        originalPos = transform.localPosition;
        //shakeDuration = maxShakeTime;
    }

    void Update()
    {
        originalPos = transform.localPosition;

        if (shakeDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * intensity;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
    }

    public void StartShake()
    {
        originalPos = transform.localPosition;
        shakeDuration = maxShakeTime;
    }

}
