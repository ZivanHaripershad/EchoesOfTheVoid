using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementsPageButton: Button
{
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    public override void OnMouseUp()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("AchievementsPage");
    }
}
