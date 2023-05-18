using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositOrbs : MonoBehaviour
{

    public OrbDepositingMode orbDepositingMode;

    public SpaceshipMode spaceshipMode;


    // Start is called before the first frame update
    void Start()
    {
        orbDepositingMode.depositingMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift)){
            orbDepositingMode.depositingMode = true;
        }
        else
        {
            orbDepositingMode.depositingMode = false;
        }
    }
}
