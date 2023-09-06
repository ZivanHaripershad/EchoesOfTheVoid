using UnityEngine;

public abstract class LevelSelectButtonInterface : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected MouseControl mouseControl;
    
    void Start()
    {
        spriteRenderer.enabled = false;
        mouseControl = GameObject.Find("MouseControl").GetComponent<MouseControl>();
        mouseControl.EnableMouse();
    }

    public abstract void OnMouseDown();

    private void OnMouseEnter()
    {
        mouseControl.EnableMouse();
        spriteRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
        spriteRenderer.enabled = false;
    }
}