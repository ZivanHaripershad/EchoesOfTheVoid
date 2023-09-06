using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLevelSelectButton : Button
{
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    public override void OnMouseUp()
    {
        //going 
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelect");
    }

}
