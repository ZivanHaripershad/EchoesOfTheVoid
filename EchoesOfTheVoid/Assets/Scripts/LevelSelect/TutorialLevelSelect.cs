using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLevelSelect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = false;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene("TutorialLevel");
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        spriteRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
        spriteRenderer.enabled = false;
    }
}
