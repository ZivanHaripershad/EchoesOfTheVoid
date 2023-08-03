using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollectOrbs : MonoBehaviour
{
    private int orbsCollected;

    [SerializeField]
    private AudioSource orbCollectSoundEffect;

    public OrbCounter orbCounter;
    public GameManagerData gameManagerData;
    
    void Start()
    {
        //set the orbs collected to 0;
        orbsCollected = 0;
        orbCounter.orbsCollected = 0;
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Orb")){
            orbCollectSoundEffect.Play();

            gameManagerData.numberOfOrbsCollected++;
            orbsCollected = ++orbCounter.orbsCollected;
            OrbCounterUI.instance.UpdateOrbs(orbsCollected);
            Destroy(collider.gameObject);
        }
        
    }
}
