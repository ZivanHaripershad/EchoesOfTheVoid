using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryLevel1Button : Button
{
    // Start is called before the first frame update
    public override void OnMouseUp()
    {
        //going 
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
    }
}
