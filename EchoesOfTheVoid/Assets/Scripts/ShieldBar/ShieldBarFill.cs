using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBarFill : MonoBehaviour
{
    public ShieldCounter shieldCounter;

    public Image fillImage;

    public Slider slider;

    void Awake()
    {
        shieldCounter.maxShieldAmount = 3;
        shieldCounter.currentShieldAmount = 0;
        shieldCounter.isShieldActive = false;
    }

    void Update()
    {
        if(slider.value == slider.minValue){
            fillImage.enabled = false;
        }

        if(shieldCounter.isShieldActive){
            if(slider.value > slider.minValue && !fillImage.enabled){
                fillImage.enabled = true;
            }

            float fillValue = shieldCounter.currentShieldAmount / (float)shieldCounter.maxShieldAmount;
            slider.value = Mathf.Clamp01(fillValue);
        }

        
    }
}
