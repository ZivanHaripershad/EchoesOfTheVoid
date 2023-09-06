using UnityEngine;

public class ResumeButton : Button
{
    [SerializeField] 
    private GameObject pauseMenu;
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    public override void OnMouseUp()
    {
        //resuming...
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
