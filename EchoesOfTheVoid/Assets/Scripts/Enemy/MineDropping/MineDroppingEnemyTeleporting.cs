using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class MineDroppingEnemyTeleporting : MonoBehaviour
{
    private GameObject[] teleportPoints;
    private Transform[] transformsPoints;
    private MineDroppingMovement mineDroppingMovement;

    [SerializeField] private float duration;
    [SerializeField] private Vector3 targetScale;
    [SerializeField] private GameObject damage;
    [SerializeField] private AudioSource damageAudio;
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject explosion;

    private float currentHealth;
    private ObjectiveManager objectiveManager;

    
    private void Start()
    {
        teleportPoints = GameObject.FindGameObjectsWithTag("MineEnemyWayPoints");
        transformsPoints = new Transform[teleportPoints.Length];
        for (int i = 0; i < teleportPoints.Length; i++)
        {
            transformsPoints[i] = teleportPoints[i].transform;
        }

        currentHealth = maxHealth;

        mineDroppingMovement = GetComponent<MineDroppingMovement>();
        objectiveManager = GameObject.FindWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            currentHealth--;
            EnemyHealthBannerUpdate();
            CheckHealth();
        }
        else if (other.gameObject.CompareTag("DoubleDamageBullet"))
        {
            currentHealth -= 2;
            EnemyHealthBannerUpdate();
            CheckHealth();
        }
    }

    private void EnemyHealthBannerUpdate()
    {
        float healthPercentage = 0f;
        if (currentHealth <= 0)
        {
            healthPercentage = 100;
        }
        else
        {
            healthPercentage = ((maxHealth - currentHealth) / maxHealth) * 100; 
        }
        
        if (healthPercentage % 20 == 0)
        {
            objectiveManager.UpdatePrimaryTargetHealthBanner(healthPercentage);
        }
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            Instantiate(damage, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1),
                Quaternion.identity);
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
            AudioManager.Instance.PlaySFX("KillMotherShip");
            AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.Level3Music);
        }
        else
        {
            Instantiate(damage, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1),
                Quaternion.identity);
            Teleport();
        }
    }

    private void Teleport()
    {
        Random random = new Random();
        int randomInt = random.Next(transformsPoints.Length - 1);
        Vector3 newTransform = transformsPoints[randomInt].transform.position;
        transform.rotation = Quaternion.LookRotation(newTransform);
        damageAudio.Play();
        StartCoroutine(ChangeScale(newTransform));
        mineDroppingMovement.UpdateWaypoint();
    }

    IEnumerator ChangeScale(Vector3 targetLocation)
    {
        Vector3 initialScale = transform.localScale;
        float elapsedTime = 0f;
        float scaleUpElapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        transform.position = new Vector3(targetLocation.x, targetLocation.y, 0);
        initialScale = Vector3.zero;
        
        while (scaleUpElapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, scaleUpElapsedTime / duration);
            scaleUpElapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
    
}
