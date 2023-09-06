using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLevelSelect : LevelSelectButtonInterface
{
    public override void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene("TutorialLevel");
    }
}
