using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultNewGameText;
    public Sprite hoveredNewGameText;
    public Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultNewGameText;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void OnMouseDown()
    {
        audioSource.Play();
        SceneManager.LoadScene("TutorialLevel");
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        spriteRenderer.sprite = hoveredNewGameText;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultNewGameText;
    }
}
