using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class AudioButton : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultAudioButtonText;
    public Sprite hoveredAudioButtonText;
    [SerializeField] private MouseControl mouseControl;
    public SettingsDataLive settingsData;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultAudioButtonText;
        mouseControl.EnableMouse();
    }

    private void OnMouseDown()
    {
        //audioSource.Play();
        //SceneManager.LoadScene("SettingsPage");

        AudioManager.Instance.PlaySFX("ButtonClick");
    }

    private void next()
    {
        settingsData.popUpIndex = 0;
    }

    private void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }

    private void OnMouseEnter()
    {
        mouseControl.EnableMouse();
        spriteRenderer.sprite = hoveredAudioButtonText;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultAudioButtonText;
    }
}
