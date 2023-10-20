using UnityEngine;

public class MotherShipDamage : MonoBehaviour
{
    [SerializeField] private MotherShipMovement motherShipMovement;
    [SerializeField] private MotherShipHealth motherShipHealth;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float delayAfterKillingMothership;
    [SerializeField] private SpriteRenderer healthWindowSp;
    [SerializeField] private MotherShipGiveShields motherShipGiveShields;
    private ObjectiveManager objectiveManager;
    public GameManagerData gameManagerData;
    [SerializeField] private GameObject damage;

    private void Start()
    {
        objectiveManager = GameObject.FindWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //damage
            motherShipHealth.TakeDamage(1);
        }
        
        if (other.gameObject.CompareTag("DoubleDamageBullet"))
        {
            //damage
            motherShipHealth.TakeDamage(2);
        }
        
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("DoubleDamageBullet"))
        {
            AudioManager.Instance.PlaySFX("MothershipDamage");

            if (motherShipHealth.IsDead())
            {
                if (gameManagerData.level.Equals(GameManagerData.Level.Level2))
                {
                    objectiveManager.UpdateMothershipDestroyedBanner();
                }
                Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                AudioManager.Instance.PlaySFX("KillMotherShip");
                AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.GamePlayMusic);
                Destroy(gameObject);
            }
            else
            {
                Instantiate(damage, gameObject.transform.position, gameObject.transform.rotation);
                motherShipMovement.StartShaking();
            }
        }
    }
}
