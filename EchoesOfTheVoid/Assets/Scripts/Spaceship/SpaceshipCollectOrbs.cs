using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollectOrbs : MonoBehaviour
{
    
    [SerializeField] private SpaceshipMode spaceshipMode;
    [SerializeField] private OrbCounter orbCounter;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider){

        if (orbCounter.orbsCollected >= GameStateManager.Instance.GetMaxOrbCapacity())
        {
            return;
        }
        
        if(collider.gameObject.CompareTag("Orb") && spaceshipMode.collectionMode){
            AudioManager.Instance.PlaySFX("OrbCollection");
            AchievementsManager.Instance.IncrementOrbsCollected();
            OrbCounterUI.GetInstance().IncrementOrbs();
            Destroy(collider.gameObject);
        }
        
    }
}
