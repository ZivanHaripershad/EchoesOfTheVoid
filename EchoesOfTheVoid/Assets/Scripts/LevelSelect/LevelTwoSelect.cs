
using UnityEngine.SceneManagement;

public class LevelTwoSelect : LevelSelectButtonInterface
{
    public override void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene("UpgradeScene2");
    }
}
