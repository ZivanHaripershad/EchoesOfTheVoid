using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokePhysics : MonoBehaviour
{
    private SpriteRenderer sp;
    
    private Vector3 lastPosition;
    private float lastTime;
    [SerializeField] private float scaleTransitionSpeed;
    [SerializeField] private float rotationTransitionSpeed;
    [SerializeField] private float rotationOffset;
    [SerializeField] private float rotationReduction;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the change in position
        Vector3 currentPosition = transform.position;
        float deltaTime = Time.time - lastTime;
        Vector3 deltaPosition = currentPosition - lastPosition;

        // Calculate velocity using the formula: velocity = change in position / change in time
        Vector3 velocity = deltaPosition / deltaTime;

        // Store the current position and time for the next frame
        lastPosition = currentPosition;
        lastTime = Time.time;

        if (!float.IsNaN(velocity.magnitude))
        {
            float scaleMultiplier = velocity.magnitude;
            Vector3 newScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);
    
            // Smoothly transition the localScale to newScale
            if (scaleMultiplier > 0)
                sp.transform.localScale = Vector3.Lerp(sp.transform.localScale, newScale, Time.deltaTime * scaleTransitionSpeed);
            
            
    
            // Rotate the sprite based on velocity direction
            if (velocity != Vector3.zero)
            {
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg + rotationOffset;
    
                if (angle != 0)
                {
                    Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    
                    // Smoothly interpolate the rotation
                    sp.transform.rotation = Quaternion.Lerp(sp.transform.rotation, targetRotation,
                        Time.deltaTime * rotationTransitionSpeed * rotationReduction);
                }
            }
        }
        
        
    }
}
