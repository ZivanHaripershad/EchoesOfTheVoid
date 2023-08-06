using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollectOrbs : MonoBehaviour
{
    [SerializeField]
    private AudioSource orbCollectSoundEffect;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Orb")){
            orbCollectSoundEffect.Play();

            OrbCounterUI.instance.IncrementOrbs();
            Destroy(collider.gameObject);
        }
        
    }
}
