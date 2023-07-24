using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositOrbs : MonoBehaviour
{

    public OrbDepositingMode orbDepositingMode;

    public SpaceshipMode spaceshipMode;

    [SerializeField]
    private AudioSource depositSoundEffect;

    [SerializeField]
    private AudioSource cannotDepositSoundEffect;

    public OrbCounter orbCounter;
    public BulletCount bulletCount;


    // Start is called before the first frame update
    void Start()
    {
        orbDepositingMode.depositingMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(spaceshipMode.collectionMode == false){
            if(Input.GetKey(KeyCode.S)){
                orbDepositingMode.depositingMode = true;
                bool deposited = false;

                //Energy
                if (Input.GetKeyDown(KeyCode.J))
                    if (orbCounter.orbsCollected >= 1)
                    {
                        orbCounter.orbsCollected -= 1;
                        orbCounter.planetOrbsDeposited += 1;
                        deposited = true;
                    }
                    else
                        cannotDepositSoundEffect.Play();

                //Ammo
                if (Input.GetKeyDown(KeyCode.I))
                    if (orbCounter.orbsCollected >= 2)
                    {
                        orbCounter.orbsCollected -= 2;
                        deposited = true;

                        bulletCount.currentBullets = bulletCount.maxBullets;
                    }
                    else
                            cannotDepositSoundEffect.Play();

                //Shield
                if (Input.GetKeyDown(KeyCode.L))
                    if (orbCounter.orbsCollected >= 3)
                    {
                        orbCounter.orbsCollected -= 3;
                        deposited = true;
                    }
                    else
                        cannotDepositSoundEffect.Play();

                if (deposited)
                {
                    //play the sound
                    depositSoundEffect.Play();

                    //update the HUD
                    OrbCounterUI.instance.UpdateOrbs(orbCounter.orbsCollected);
                }
                
            }
            else
            {
                orbDepositingMode.depositingMode = false;
            }
        }
        
    }
}
