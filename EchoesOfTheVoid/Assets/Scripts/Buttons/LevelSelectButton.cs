using UnityEngine.SceneManagement;

public class LevelSelectButton : Button
{
    private void next()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
     
    public override void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }
}
