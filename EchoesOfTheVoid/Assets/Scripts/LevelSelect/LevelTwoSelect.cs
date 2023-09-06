
public class LevelTwoSelect : LevelSelectButtonInterface
{
    public override void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("CannotDeposit");
        // SceneManager.LoadScene("Level2");
    }
}
