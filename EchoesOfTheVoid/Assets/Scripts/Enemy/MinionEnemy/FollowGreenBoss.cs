using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FollowGreenBoss : MonoBehaviour
{

    [SerializeField] private GameObject explosion;
    private GameObject greenBoss;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float followXOffset;
    [SerializeField] private float followYOffset;
    [SerializeField] private float shieldDepositTime;
    [SerializeField] private GameObject minionShield;

    private float cooldown;
    [SerializeField] private float destroyDelayAfterGreenDies;

    [SerializeField] private float maxCooldown; 

    void Start()
    {
        greenBoss = GameObject.FindGameObjectWithTag("MineEnemy");
        cooldown = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        
        //find the boss
        if (greenBoss != null)
        {
            Vector3 greenPosition = greenBoss.transform.position;
            Vector3 myPosition = gameObject.transform.position;
        
            // Use Lerp to smoothly move towards the target position
            transform.position = Vector3.Lerp(myPosition, new Vector3(greenPosition.x + followXOffset, greenPosition.y + followXOffset, greenPosition.z), smoothSpeed * Time.deltaTime);

            cooldown -= Time.deltaTime;
            
            if (cooldown <= 0)
            {
                cooldown = maxCooldown;
                Transform pos = gameObject.transform;
                if (!GameObject.FindGameObjectWithTag("MinionShield"))
                    Instantiate(minionShield, pos.position, pos.rotation);
            }
        }
        else //if not found, self-destruct
        {
            Invoke(nameof(DestroyMe), destroyDelayAfterGreenDies);
        }
        
        
        
    }

    private void DestroyMe()
    {
        Instantiate(explosion, transform.position, quaternion.identity);
        AudioManager.Instance.PlaySFX("DestroyEnemy");
        Destroy(gameObject);
    }
}
