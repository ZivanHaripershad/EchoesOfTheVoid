using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultMainMenuText;
    public Sprite hoveredMainMenueText;
    [SerializeField] private MouseControl mouseControl;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultMainMenuText;
        mouseControl.EnableMouse();
    }

    private void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene("MainMenu");
    }

    private void OnMouseEnter()
    {
        mouseControl.EnableMouse();
        spriteRenderer.sprite = hoveredMainMenueText;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultMainMenuText;
    }
}
