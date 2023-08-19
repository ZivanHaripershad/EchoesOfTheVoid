using UnityEngine;
using UnityEngine.SceneManagement;


public class BackToMainMenu : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultExitGameText;
    public Sprite hoveredExitGameText;
    public Texture2D cursorTexture;
    public AudioSource audioSource;
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
        audioSource.Play();
        SceneManager.LoadScene("MainMenu");
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