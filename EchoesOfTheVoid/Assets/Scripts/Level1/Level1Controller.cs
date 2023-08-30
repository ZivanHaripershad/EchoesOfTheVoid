using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : LevelController
{
    
    [SerializeField]
    private MouseControl mouseControl;
    
    // Start is called before the first frame update
    void Start()
    {
        missionPopup.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
