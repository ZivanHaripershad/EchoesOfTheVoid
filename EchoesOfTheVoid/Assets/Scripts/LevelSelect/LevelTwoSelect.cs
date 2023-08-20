using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTwoSelect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = false;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void OnMouseDown()
    {
        audioSource.Play();
        // SceneManager.LoadScene("TutorialLevel");
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
