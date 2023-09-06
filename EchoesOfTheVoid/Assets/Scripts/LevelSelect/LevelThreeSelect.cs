
public class LevelThreeSelect : LevelSelectButtonInterface
{
    public override void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("CannotDeposit");
        // SceneManager.LoadScene("Level3");
    }
}
