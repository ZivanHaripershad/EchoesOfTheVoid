using UnityEngine;

public class ExitGameButton : Button
{
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    public override void OnMouseUp()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}