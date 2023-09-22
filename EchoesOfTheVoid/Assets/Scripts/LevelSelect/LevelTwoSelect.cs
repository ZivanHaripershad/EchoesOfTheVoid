
using UnityEngine.SceneManagement;

public class LevelTwoSelect : LevelSelectButtonInterface
{
    public override void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("ButtonClicked");
        SceneManager.LoadScene("Level2");
    }
}
