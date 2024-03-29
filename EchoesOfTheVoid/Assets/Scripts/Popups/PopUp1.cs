public class PopUp1 : Button
{
   
    public TutorialData tutorialData;
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }

    private void Next()
    {
        tutorialData.popUpIndex = 1;
    }
    
    public override void OnMouseUp()
    {
        Invoke("Next", 0.3f);
    }
    
}
