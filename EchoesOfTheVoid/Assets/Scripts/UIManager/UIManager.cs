using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject earth;
    public GameObject spaceship;
    void Start()
    {
        earth.SetActive(false);
        spaceship.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
