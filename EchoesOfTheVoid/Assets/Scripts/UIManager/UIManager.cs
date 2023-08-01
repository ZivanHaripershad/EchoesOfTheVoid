using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject earth;
    public GameObject spaceship;
    public AtmosphereReaction atmosphereReaction;
    public GameObject bulletFactory;
    public GameObject powerFactory;
    public GameObject shieldFactory;
    public GameObject healthFactory;
    void Start()
    {
        earth.SetActive(false);
        spaceship.SetActive(false);
        bulletFactory.SetActive(false);
        powerFactory.SetActive(false);
        shieldFactory.SetActive(false);
        healthFactory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelObjectsToActive()
    {
        earth.SetActive(true);
        spaceship.SetActive(true);
    }
}
