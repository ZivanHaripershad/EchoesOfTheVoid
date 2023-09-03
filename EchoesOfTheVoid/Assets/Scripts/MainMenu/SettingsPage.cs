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

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultSettingsPage;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        GameObject.FindGameObjectWithTag("MouseControl").GetComponent<MouseControl>().EnableMouse();
    }

    private void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
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
