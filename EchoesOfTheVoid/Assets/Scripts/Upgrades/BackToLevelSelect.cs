using UnityEngine;
using UnityEngine.SceneManagement;


public class BackToLevelSelect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultBackText;
    public Sprite hoveredBackText;
    public Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultBackText;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        GameObject.FindGameObjectWithTag("MouseControl").GetComponent<MouseControl>().EnableMouse();
    }

    private void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene("LevelSelect");
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        spriteRenderer.sprite = hoveredBackText;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultBackText;
    }
}