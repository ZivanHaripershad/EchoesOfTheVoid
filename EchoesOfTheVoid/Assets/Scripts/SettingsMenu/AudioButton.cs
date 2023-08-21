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
    public Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;
    public AudioSource audioSource;
    public SettingsDataLive settingsData;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultAudioButtonText;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        
    }

    private void OnMouseDown()
    {
        //audioSource.Play();
        //SceneManager.LoadScene("SettingsPage");

        AudioManager.Instance.PlaySFX("Click");

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
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        spriteRenderer.sprite = hoveredAudioButtonText;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultAudioButtonText;
    }
}
