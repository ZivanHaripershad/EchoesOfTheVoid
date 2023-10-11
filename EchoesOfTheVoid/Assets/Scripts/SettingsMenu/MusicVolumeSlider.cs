
using UnityEngine;
using System.Linq;
using Slider = UnityEngine.UI.Slider;


public class MusicVolumeSlider : MonoBehaviour
{

    private Slider musicSlider;
    
    void Start()
    {
        musicSlider = FindObjectsOfType<Slider>().ToList().Find( x=>x.name == "BackGroundSlider");
        musicSlider.value = AudioManager.Instance.currentMusicSliderValue;
    }
}
