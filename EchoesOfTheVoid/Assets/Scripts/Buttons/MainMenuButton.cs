using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultNewGameText;
    public Sprite hoveredNewGameText;
    public MouseControl mouseControl;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultNewGameText;
        mouseControl.EnableMouse();
    }

    private void OnMouseDown()
    {
        Debug.Log("going to main menu");
        audioSource.Play();
        SceneManager.LoadScene("MainMenu");
    }

    private void OnMouseEnter()
    {
        mouseControl.EnableMouse();
        spriteRenderer.sprite = hoveredNewGameText;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultNewGameText;
    }
}
