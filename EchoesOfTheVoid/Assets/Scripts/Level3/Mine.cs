using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField]
    private float explodeDelay;

    [SerializeField] 
    private float explosionRadius;

    [SerializeField] private float detectionRadius;

    [SerializeField] private GameObject explosion;

    private GameObject player;
    private CameraShake cameraShake;

    private bool isCountingDown;
    [SerializeField] private float expirationTime;

    private EnemySpeedControl enemySpeedControl;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySFX("PlantMine");
        isCountingDown = false;
        InvokeRepeating("CheckMine", 0f, 0.1f);
        player = GameObject.FindGameObjectWithTag("Player");
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        enemySpeedControl = GameObject.FindGameObjectWithTag("EnemySpeedControl").GetComponent<EnemySpeedControl>();
    }

    private void Update()
    {
        expirationTime -= Time.deltaTime * enemySpeedControl.GetCurrentTimeScale();
        if (expirationTime < 0)
            Explode();
    }


    void CheckMine()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        if (isCountingDown == false)
        {
            //player enters the detection radius of the mine, 
            //start counting down
            if (GetDistance() < detectionRadius)
                isCountingDown = true;
        }
        else
        {
            explodeDelay -= Time.deltaTime;

            if (explodeDelay <= 0)
            {
                AudioManager.Instance.PlaySFX("MineExplodes");
                Explode();
            }
        }
        
    }

    private void Explode()
    {
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

        //player in the blast radius of the mine
        if (GetDistance() < explosionRadius)
        {
            //set off mine to explode
            cameraShake.Shake();
            player.GetComponent<SpaceshipCollection>().Stun();
        }
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            AudioManager.Instance.PlaySFX("MineExplodesNoBeep");
            Explode();
        }
    }

    private float GetDistance()
    {
        return Vector3.Distance(player.transform.position, gameObject.transform.position);
    }
    
}
