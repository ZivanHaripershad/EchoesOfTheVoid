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

    private void Start()
    {
        teleportPoints = GameObject.FindGameObjectsWithTag("MineEnemyWayPoints");
        transformsPoints = new Transform[teleportPoints.Length];
        for (int i = 0; i < teleportPoints.Length; i++)
        {
            transformsPoints[i] = teleportPoints[i].transform;
        }

        mineDroppingMovement = GetComponent<MineDroppingMovement>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Instantiate(damage, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.identity);
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
