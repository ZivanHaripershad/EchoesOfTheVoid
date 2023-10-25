using UnityEngine;

public class MotherShipDamage : MonoBehaviour
{
    [SerializeField] private MotherShipMovement motherShipMovement;
    [SerializeField] private MotherShipHealth motherShipHealth;
    [SerializeField] private GameObject explosion;
    private ObjectiveManager objectiveManager;
    public GameManagerData gameManagerData;
    [SerializeField] private GameObject damage;
    
    [SerializeField] private GameObject minusOne;
    [SerializeField] private GameObject minusTwo;

    private void Start()
    {
        objectiveManager = GameObject.FindWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
    }
    
    private void DamageNumber(GameObject number, Collider2D other)
    {
        GameObject dmg = Instantiate(number, gameObject.transform.position, gameObject.transform.rotation);
        dmg.GetComponent<DamageNumber>().AddBulletForce(other.transform.rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //damage
            motherShipHealth.TakeDamage(1);
            DamageNumber(minusOne, other);
        }
        
        if (other.gameObject.CompareTag("DoubleDamageBullet"))
        {
            //damage
            motherShipHealth.TakeDamage(2);
            DamageNumber(minusTwo, other);
        }
        
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("DoubleDamageBullet"))
        {
            AudioManager.Instance.PlaySFX("MothershipDamage");

            if (motherShipHealth.IsDead())
            {
                if (GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level2))
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
