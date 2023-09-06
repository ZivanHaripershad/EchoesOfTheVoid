using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPageButton: Button
{
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    public override void OnMouseUp()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SettingsPage");
    }
}
