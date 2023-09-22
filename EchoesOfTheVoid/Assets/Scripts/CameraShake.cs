using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
   
    private Transform camTransform;
	
    // How long the object should shake for.
    [SerializeField] private float shakeDuration;
	
    // Amplitude of the shake. A larger value shakes the camera harder.
    [SerializeField] private float shakeAmount;
    [SerializeField] private float decreaseFactor;

    private float currentShake;
	
    Vector3 originalPos;
	
    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }
	
    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    public void Shake()
    {
        currentShake = shakeDuration;
    }

    private void Start()
    {
        currentShake = 0; 
    }

    void Update()
    {
        if (currentShake > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeAmount *= decreaseFactor;
            currentShake -= Time.deltaTime;
        }
        else
        {
            currentShake = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}
