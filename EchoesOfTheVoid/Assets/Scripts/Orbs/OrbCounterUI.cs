using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbCounterUI : MonoBehaviour
{
    public static OrbCounterUI instance;

    public Text orbCounterText;

    void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        orbCounterText.text = "0";
    }

    // Update is called once per frame
    public void UpdateOrbs(int orbs)
    {
        orbCounterText.text = orbs.ToString();
    }
}
