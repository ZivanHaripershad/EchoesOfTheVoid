using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultExitGameText;
    public Sprite hoveredExitGameText;
    public Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultExitGameText;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        Application.Quit();
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        spriteRenderer.sprite = hoveredExitGameText;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultExitGameText;
    }
}