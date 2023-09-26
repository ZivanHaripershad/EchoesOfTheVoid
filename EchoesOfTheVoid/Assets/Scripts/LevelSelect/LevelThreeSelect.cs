using UnityEngine.SceneManagement;

public class LevelThreeSelect : LevelSelectButtonInterface
{
    public override void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene("Level3");
    }
}
