using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class CreditsButton : MonoBehaviour
{
   // public static ControlButton Instance;
    public SpriteRenderer spriteRenderer;
    public Sprite defaultControlButtonText;
    public Sprite hoveredControlButtonText;
    public SettingsDataLive settingsData;
    [SerializeField] private MouseControl mouseControl;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultControlButtonText;
        mouseControl.EnableMouse();
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
        settingsData.popUpIndex = 2;
    }

    private void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }

    private void OnMouseEnter()
    {
        mouseControl.EnableMouse();
        spriteRenderer.sprite = hoveredControlButtonText;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultControlButtonText;
    }
}
