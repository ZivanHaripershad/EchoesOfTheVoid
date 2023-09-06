public class Level1Popup2 : Button
{
    // Start is called before the first frame update
    public Level1Data level1Data;

    private void Next()
    {
        level1Data.popUpIndex = 1;
    }
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    public override void OnMouseUp()
    {
        Invoke("Next", 0.3f);
    }

}
