using UnityEngine.SceneManagement;

public class Level2NextLevelButton : Button
{
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    private void next()
    {
        SceneManager.LoadScene("UpgradeScene3");
    }
    
    public override void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }
}
