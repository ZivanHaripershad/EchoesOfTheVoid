using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FillPowerBar : MonoBehaviour
{
    public OrbCounter orbCounter;

    public Image fillImage;

    public Slider slider;

    // Start is called before the first frame update
    void Awake()
    {
        orbCounter.planetOrbMax = 10;
        orbCounter.planetOrbsDeposited = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value == slider.minValue){
            fillImage.enabled = false;
        }

        if(slider.value > slider.minValue && !fillImage.enabled){
            fillImage.enabled = true;
        }

        float fillValue = orbCounter.planetOrbsDeposited / (float)orbCounter.planetOrbMax;
        slider.value = Mathf.Clamp01(fillValue);
    }
}
