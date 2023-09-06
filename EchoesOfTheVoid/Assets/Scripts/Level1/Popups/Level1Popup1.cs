
public class Level1Popup1 : Button
{
    // Start is called before the first frame updat
    public Level1Data level1Data;
    

    private void next()
    {
        level1Data.popUpIndex = 1;
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
