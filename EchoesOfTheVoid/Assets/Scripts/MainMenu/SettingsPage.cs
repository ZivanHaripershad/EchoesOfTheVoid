using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPage: MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultSettingsPage;
    public Sprite hoveredSettingsPage;
    public Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultSettingsPage;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void OnMouseDown()
    {
        audioSource.Play();
        SceneManager.LoadScene("SettingsPage");
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        spriteRenderer.sprite = hoveredSettingsPage;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultSettingsPage;
    }
}
