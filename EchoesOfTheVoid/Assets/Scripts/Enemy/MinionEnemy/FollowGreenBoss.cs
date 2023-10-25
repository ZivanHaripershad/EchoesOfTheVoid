using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGreenBoss : MonoBehaviour
{

    [SerializeField] private GameObject explosion;
    private GameObject greenBoss;
    [SerializeField] private float smoothSpeed;

    void Start()
    {
        greenBoss = GameObject.FindGameObjectWithTag("MineEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("Following green");
        //find the boss
        if (greenBoss != null)
        {
            Vector3 greenPosition = greenBoss.transform.position;
            Vector3 targetPosition = new Vector3(greenPosition.x, greenPosition.y, 0);
            Vector3 myPosition = gameObject.transform.position;
    
            // Use Lerp to smoothly move towards the target position
            transform.position = Vector3.Lerp(new Vector3(myPosition.x, myPosition.y + 10.5f, myPosition.z), targetPosition, smoothSpeed * Time.deltaTime);
        }
        else //if not found, self-destruct
        {
            Instantiate(explosion);
            AudioManager.Instance.PlaySFX("DestroyEnemy");
            Destroy(gameObject);
        }
        
        
        
    }
}
