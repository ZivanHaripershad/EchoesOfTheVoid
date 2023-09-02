using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResumeButton : Button
{
    [SerializeField] 
    private GameObject pauseMenu;
    
    public override void OnMouseUp()
    {
        //resuming...
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
