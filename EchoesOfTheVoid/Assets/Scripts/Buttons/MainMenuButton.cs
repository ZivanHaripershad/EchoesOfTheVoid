using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : Button
{
    private void next()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public override void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }
}
