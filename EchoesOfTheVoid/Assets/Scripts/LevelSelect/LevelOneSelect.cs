using UnityEngine.SceneManagement;

public class LevelOneSelect : LevelSelectButtonInterface
{
    public override void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene("UpgradeScene1");
    }
}
