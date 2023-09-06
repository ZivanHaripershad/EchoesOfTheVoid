using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTutorial : Button
{
    // Start is called before the first frame update
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
