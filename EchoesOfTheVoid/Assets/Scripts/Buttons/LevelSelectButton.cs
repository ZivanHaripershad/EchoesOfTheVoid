using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : Button
{
    private void next()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    override 
        public void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }
}
