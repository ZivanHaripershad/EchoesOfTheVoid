using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private GameObject mineEnemy;

    [SerializeField] private float followSpeed;
    [SerializeField] private float jitterAdditionSpeed;
    [SerializeField] private float randomJitter;

    private float currentJitterX; 
    private float currentJitterY; 

    // Start is called before the first frame update
    void Start()
    {
        mineEnemy = GameObject.FindGameObjectWithTag("MineEnemyShieldCenter");
    }

    // Update is called once per frame
    void Update()
    {
        if (mineEnemy == null)
            Destroy(gameObject);

        float jitterX = Random.Range(-randomJitter, randomJitter);
        float jitterY = Random.Range(-randomJitter, randomJitter);

        currentJitterX += jitterX;
        currentJitterY += jitterY;

        if (currentJitterX > randomJitter)
            currentJitterX = randomJitter;
        if (currentJitterY > randomJitter)
            currentJitterY = randomJitter;
        
        if (currentJitterX < -randomJitter)
            currentJitterX = -randomJitter;
        if (currentJitterY < -randomJitter)
            currentJitterY = -randomJitter;
        
        transform.position = Vector3.Lerp(transform.position, new Vector3(mineEnemy.transform.position.x, mineEnemy.transform.position.y, 0), followSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, new Vector3(mineEnemy.transform.position.x + jitterX, mineEnemy.transform.position.y + jitterY, 0), jitterAdditionSpeed * Time.deltaTime);

    }
}
