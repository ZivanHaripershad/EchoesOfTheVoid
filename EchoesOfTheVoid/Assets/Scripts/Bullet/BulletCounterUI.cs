using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounterUI : MonoBehaviour
{
    public static BulletCounterUI instance;

    public Text bulletCounterText;

    void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletCounterText.text = "10";
    }

    // Update is called once per frame
    public void UpdateBullets(int bullets)
    {
        bulletCounterText.text = bullets.ToString();
    }
}
