
using UnityEngine;
using System.Linq;
using Slider = UnityEngine.UI.Slider;


public class SfxVolumeSlider : MonoBehaviour
{

    private Slider sfxSlider;
    
    void Start()
    {
        sfxSlider = FindObjectsOfType<Slider>().ToList().Find( x=>x.name == "EffectsSlider");
        sfxSlider.value = AudioManager.Instance.currentSfxSliderValue;
    }
}
