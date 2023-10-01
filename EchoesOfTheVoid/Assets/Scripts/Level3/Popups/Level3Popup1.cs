public class Level3Popup1: Button
{
    // Start is called before the first frame updat
    public Level3Data level3Data;
    

    private void next()
    {
        level3Data.popUpIndex = 1;
    }
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }

    override 
    public void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }
    
}
