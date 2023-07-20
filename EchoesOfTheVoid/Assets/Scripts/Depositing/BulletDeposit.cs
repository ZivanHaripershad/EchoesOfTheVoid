using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeposit : MonoBehaviour
{
    public OrbCounter orbCounter;
    public FactoryCosts factoryCosts;
    public BulletCount bulletCount;
    public OrbCounter orbCount; 

    [SerializeField]
    private AudioSource depositSoundEffect;

    [SerializeField]
    Animator animator;

    void Start()
    {
        animator.enabled = false;
    }

    // Start is called before the first frame update
    public void OnMouseDown(){
        if(orbCounter.orbsCollected >= factoryCosts.bulletCost){
            orbCounter.orbsCollected = orbCounter.orbsCollected - factoryCosts.bulletCost;
            OrbCounterUI.instance.UpdateOrbs(orbCounter.orbsCollected);

            bulletCount.currentBullets = bulletCount.maxBullets;
            BulletCounterUI.instance.UpdateBullets(bulletCount.currentBullets);

            depositSoundEffect.Play();
        }
    }

    public void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.6f, 1.6f, 0f); //adjust these values as you see fit
    }


    public void OnMouseExit()
    {
        transform.localScale = new Vector3(1.3f, 1.3f, 1f);  // assuming you want it to return to its original size when your mouse leaves it.
    }
 
}
