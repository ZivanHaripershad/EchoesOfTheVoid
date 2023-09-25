
public class Level2Popup1 : Button
{
    // Start is called before the first frame updat
    public Level2Data level2Data;
    

    private void next()
    {
        level2Data.popUpIndex = 1;
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
