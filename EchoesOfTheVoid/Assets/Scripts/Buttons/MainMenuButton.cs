using UnityEngine.SceneManagement;

public class MainMenuButton : Button
{
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    private void next()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public override void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }
}
