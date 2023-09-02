using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultLevelSelect;
    public Sprite hoveredLevelSelect;
    public Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultLevelSelect;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void OnMouseDown()
    {
        audioSource.Play();
        //SceneManager.LoadScene("LevelSelect");
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        spriteRenderer.sprite = hoveredLevelSelect;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultLevelSelect;
    }
}
