public class Level3Popup2: Button
{
    
    // Start is called before the first frame update
    public Level3Data level3Data;
    
    private void Next()
    {
        level3Data.popUpIndex = 2;
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
