using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class FillPowerBar : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    private OrbCounter counter;

    private SpriteRenderer spriteRenderer;

    private int prevSprite; 

    // Start is called before the first frame update

    [SerializeField]
    private float barFillSpeed;

    [SerializeField] 
    private float fillSoundVolume;

    private float currTimeFilled;

    private AudioSource[] fillEnergySound;

    [SerializeField] 
    private float fillSoundFadeSpeed;

    [SerializeField] 
    private Animator haloAnimator;

    private bool isStillFilling;
    
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private float reduceSpawnIntervalAndVariation;
    [SerializeField] private float reduceTimeTillNextWave;

    [SerializeField] private int firstFilledSpriteNumber;
    [SerializeField] private int secondFilledSpriteNumber;
    [SerializeField] private int thirdFilledSpriteNumber;
    [SerializeField] private  int fourthFilledSpriteNumber;
    [SerializeField] private  int fifthFilledSpriteNumber;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        prevSprite = 0;
        fillEnergySound = GetComponents<AudioSource>();
        isStillFilling = true;
    }

    public bool IsStillFilling()
    {
        return isStillFilling;
    }


    void Awake()
    {
        counter.planetOrbMax = 10;
        counter.planetOrbsDeposited = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int currSprite = (int) Mathf.Round((counter.planetOrbsDeposited * 1.0f / counter.planetOrbMax) * (sprites.Length - 1));

        if (prevSprite > currSprite)
        {
            prevSprite = currSprite;
        }
        
        if (prevSprite < currSprite)
        {
            
            fillEnergySound[5].volume = fillSoundVolume;
            if (!fillEnergySound[5].isPlaying)
                fillEnergySound[5].Play();//Shepherd tone
            
            currTimeFilled += Time.deltaTime;

            if (currTimeFilled > (1/barFillSpeed))
            {
                currTimeFilled = 0;
                prevSprite++;
                
                if (prevSprite == firstFilledSpriteNumber)
                {
                    fillEnergySound[0].Play();
                    ReduceEnemySpawning();
                }

                if (prevSprite == secondFilledSpriteNumber)
                {
                    fillEnergySound[1].Play();
                    ReduceEnemySpawning();
                }

                if (prevSprite == thirdFilledSpriteNumber)
                {
                    fillEnergySound[2].Play();
                    ReduceEnemySpawning();
                }

                if (prevSprite == fourthFilledSpriteNumber)
                {
                    fillEnergySound[3].Play();
                    ReduceEnemySpawning();
                }

                if (prevSprite == fifthFilledSpriteNumber)
                {
                    fillEnergySound[4].Play();
                    ReduceEnemySpawning();
                }
            }
            
            haloAnimator.SetBool("mustGlow", true);
        }
        else
        {
            fillEnergySound[5].volume -= fillSoundFadeSpeed * Time.deltaTime;
            haloAnimator.SetBool("mustGlow", false);
        }

        if (prevSprite > sprites.Length - 1)
            prevSprite = sprites.Length - 1;
        
        spriteRenderer.sprite = sprites[prevSprite];

        if (prevSprite == sprites.Length - 1)
            isStillFilling = false;
    }

    private void ReduceEnemySpawning()
    {
        gameManagerData.spawnInterval *= reduceSpawnIntervalAndVariation;
        gameManagerData.spawnTimerVariation *= reduceSpawnIntervalAndVariation;
        gameManagerData.timeTillNextWave *= reduceTimeTillNextWave;
    }
}
