using UnityEngine;

public class MotherShipDamage : MonoBehaviour
{
    [SerializeField] private MotherShipMovement motherShipMovement;
    [SerializeField] private AudioSource hitBoss;
    [SerializeField] private AudioSource killMotherShip;
    [SerializeField] private MotherShipHealth motherShipHealth;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float delayAfterKillingMothership;
    [SerializeField] private SpriteRenderer healthWindowSp;
    [SerializeField] private MotherShipGiveShields motherShipGiveShields;
    

    private void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //damage
            hitBoss.Play();
            motherShipHealth.TakeDamage();

            if (motherShipHealth.IsDead())
            {
                Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                AudioManager.Instance.PlaySFX("KillMotherShip");
                AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.GamePlayMusic);
                Destroy(gameObject);
            }
            else 
                motherShipMovement.StartShaking();
        }
    }
}
