using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class ControlButton : MonoBehaviour
{
   // public static ControlButton Instance;
    public SpriteRenderer spriteRenderer;
    public Sprite defaultControlButtonText;
    public Sprite hoveredControlButtonText;
    public Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;
    public SettingsDataLive settingsData;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultControlButtonText;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        
    }

    public void OnMouseDown()
    {
        //audioSource.Play();
        // gameObject.parent.parent.SetActive(false);
        // SettingsController.Instance.SetPopUpIndex(1);
        AudioManager.Instance.PlaySFX("ButtonClick");
        //SettingsController.Instance.popUpIndex = 1;

    }

    private void next()
    {
        settingsData.popUpIndex = 1;
    }

    private void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        spriteRenderer.sprite = hoveredControlButtonText;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultControlButtonText;
    }
}
