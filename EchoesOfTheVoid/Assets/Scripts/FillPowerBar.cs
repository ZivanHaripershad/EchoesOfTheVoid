using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class FillPowerBar : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    private OrbCounter counter;

    [SerializeField]
    private float scale;

    [SerializeField]
    private float yAdjust;

    private SpriteRenderer spriteRenderer;

    private int prevSprite; 

    // Start is called before the first frame update

    [SerializeField]
    private float barFillSpeed;

    [SerializeField] private float fillSoundVolume;

    private float currTimeFilled;

    private AudioSource[] fillEnergySound;

    [SerializeField] private float fillSoundFadeSpeed;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        prevSprite = 0;      
        Vector2 newScale = new Vector2 (scale, scale);
        transform.localScale = new Vector3(newScale.x, newScale.y, 1f);
        transform.position += new Vector3(0f, yAdjust, 0f);
        fillEnergySound = GetComponents<AudioSource>();
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
            }

            if (prevSprite == 7) 
                fillEnergySound[0].Play();

            if (prevSprite == 38)
                fillEnergySound[1].Play();
            
            if (prevSprite == 69)
                fillEnergySound[2].Play();

            if (prevSprite == 101)
                fillEnergySound[3].Play();
            
            if (prevSprite == 142)
                fillEnergySound[4].Play();
        }
        else
        {
            fillEnergySound[5].volume -= fillSoundFadeSpeed * Time.deltaTime;
        }

        if (prevSprite > sprites.Length - 1)
            prevSprite = sprites.Length - 1;
        
        spriteRenderer.sprite = sprites[prevSprite];
    }
}
