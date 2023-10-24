using UnityEngine;

public class MarkingModeButton : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite markingModeOn;
    [SerializeField] protected Sprite markingModeOff;
    [SerializeField] protected MouseControl mouseControl;
    
    private void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    private void OnMouseUp()
    {
        GameStateManager.Instance.IsMarkingModeOn = !GameStateManager.Instance.IsMarkingModeOn;

        if (GameStateManager.Instance.IsMarkingModeOn)
        {
            spriteRenderer.sprite = markingModeOn;
        }
        else
        {
            spriteRenderer.sprite = markingModeOff;
        }
    }
}
