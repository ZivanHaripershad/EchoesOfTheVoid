using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollectOrbs : MonoBehaviour
{
    public int orbsCollected;

    [SerializeField]
    private AudioSource orbCollectSoundEffect;

    void Start()
    {
        //set the orbs collected to 0;
        orbsCollected = 0;
        
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Orb"){
            orbCollectSoundEffect.Play();
            Debug.Log("Collided");
            OrbCounterUI.instance.IncreaseOrbs(++orbsCollected);
            Destroy(collider.gameObject);
        }
        
    }
}
