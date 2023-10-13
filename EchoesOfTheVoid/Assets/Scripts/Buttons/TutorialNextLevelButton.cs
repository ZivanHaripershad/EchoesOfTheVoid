using UnityEngine.SceneManagement;

public class TutorialNextLevelButton : Button
{
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    private void next()
    {
        SceneManager.LoadScene("UpgradeScene1");
    }
    
    public override void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }
}
