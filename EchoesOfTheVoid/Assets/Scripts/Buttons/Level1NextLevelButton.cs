using UnityEngine.SceneManagement;

public class Level1NextLevelButton : Button
{
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    private void next()
    {
        SceneManager.LoadScene("UpgradeScene2");
    }
    
    public override void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }
}
