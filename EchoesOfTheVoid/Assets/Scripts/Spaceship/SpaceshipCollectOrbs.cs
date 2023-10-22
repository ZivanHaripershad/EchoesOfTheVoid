using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollectOrbs : MonoBehaviour
{
    
    [SerializeField] private SpaceshipMode spaceshipMode;
    [SerializeField] private OrbCounter orbCounter;
    [SerializeField] private HealthCount healthCount;
    [SerializeField] private Animator earthDamageAnimator;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider){
        
        if(collider.gameObject.CompareTag("Orb") && spaceshipMode.collectionMode){
            
            if (orbCounter.orbsCollected >= GameStateManager.Instance.GetMaxOrbCapacity())
            {
                AudioManager.Instance.PlaySFX("CannotCollect");
                return;
            }
            
            AudioManager.Instance.PlaySFX("OrbCollection");
            AchievementsManager.Instance.IncrementOrbsCollected();
            OrbCounterUI.GetInstance().IncrementOrbs();
            Destroy(collider.gameObject);
        }
        
        if(collider.gameObject.CompareTag("HealthOrb") && spaceshipMode.collectionMode){
            
            if (healthCount.currentHealth >= healthCount.maxHealth)
            {
                AudioManager.Instance.PlaySFX("CannotCollect");
                return;
            }
            
            healthCount.currentHealth++;
            AudioManager.Instance.PlaySFX("CollectHealth");
            CheckHealth();
            Destroy(collider.gameObject);
        }
        
    }
    
    private void CheckHealth()
    {
        if (healthCount.currentHealth > healthCount.maxHealth * 0.1) //20% damage
            earthDamageAnimator.SetBool("damage1", false);
            
        if (healthCount.currentHealth > healthCount.maxHealth * 0.2) //40% damage
            earthDamageAnimator.SetBool("damage2", false);
        
        if (healthCount.currentHealth > healthCount.maxHealth * 0.4) //60% damage
            earthDamageAnimator.SetBool("damage3", false);
        
        if (healthCount.currentHealth > healthCount.maxHealth * 0.6) //80% damage
            earthDamageAnimator.SetBool("damage4", false);
        
        if (healthCount.currentHealth > healthCount.maxHealth * 0.8) //90% damage
            earthDamageAnimator.SetBool("damage5", false);
    }
}
