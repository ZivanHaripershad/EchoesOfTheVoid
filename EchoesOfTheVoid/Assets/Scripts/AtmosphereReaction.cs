using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AtmosphereReaction : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletFactory;

    [SerializeField]
    private GameObject powerFactory;

    [SerializeField]
    private GameObject shieldFactory;

    [SerializeField]
    private GameObject healthFactory;
    
    [SerializeField] private GameObject networkObjective;
    [SerializeField] private GameObject healthObjective;
    
    //level1
    [SerializeField] private GameObject level1EnemyObjective;
    
    //level2
    [SerializeField] private GameObject level2EnemyObjective;

    //level3
    [SerializeField] private GameObject level3EnemyObjective;
    
    [SerializeField] private BulletDeposit bulletDeposit;
    [SerializeField] private HealthDeposit healthDeposit;
    [SerializeField] private PowerDeposit powerDeposit;
    [SerializeField] private ShieldDeposit shieldDeposit;
    [SerializeField] private GameObject darkenBackground;
    [SerializeField] private float darkenBackgroundAlpha;
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDuration;
    
    //level1
    [SerializeField] private GameObject level1Upgrade;
    
    //level2
    [SerializeField] private GameObject level2Upgrade;

    //level3
    [SerializeField] private GameObject level3Upgrade;
    
    [SerializeField] private GameManagerData gameManagerData;
    
    public OrbDepositingMode orbDepositingMode;

    private bool isUp;

    public bool IsUp()
    {
        return isUp;
    }

    IEnumerator Fade(GameObject gameObject, float startAlpha, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();

        // Gradually fade the renderers
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration); ;
            
            foreach (var renderer in renderers)
            {
                Color color = renderer.material.color;
                color.a = alpha;
                renderer.material.color = color;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // After the fade-out effect is complete, disable the game object
        if (targetAlpha == 0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        isUp = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && orbDepositingMode.depositingMode)
        {
            isUp = true;
            if (AudioManager.Instance)
                AudioManager.Instance.ReduceAudioSpeed();
            
            bulletDeposit.GetComponent<BulletDeposit>().RenderSprites();
            bulletFactory.SetActive(true);
            powerDeposit.GetComponent<PowerDeposit>().RenderSprites();
            powerFactory.SetActive(true);
            shieldDeposit.GetComponent<ShieldDeposit>().RenderSprites();
            shieldFactory.SetActive(true);
            healthDeposit.GetComponent<HealthDeposit>().RenderSprites();
            healthFactory.SetActive(true);
            
            darkenBackground.SetActive(true);

            if (networkObjective)
            {
                networkObjective.SetActive(true);
                StartCoroutine(Fade(networkObjective, 0f, 1f, fadeInDuration));
            }

            if (healthObjective)
            {
                healthObjective.SetActive(true);
                StartCoroutine(Fade(healthObjective, 0f, 1f, fadeInDuration));
            }

            if (level1EnemyObjective)
            {
                level1EnemyObjective.SetActive(true);
                StartCoroutine(Fade(level1EnemyObjective, 0f, 1f, fadeInDuration));
            }
            
            if (level2EnemyObjective)
            {
                level2EnemyObjective.SetActive(true);
                StartCoroutine(Fade(level2EnemyObjective, 0f, 1f, fadeInDuration));
            }
            
            if (level3EnemyObjective)
            {
                level3EnemyObjective.SetActive(true);
                StartCoroutine(Fade(level3EnemyObjective, 0f, 1f, fadeInDuration));
            }

            if (gameManagerData.level.Equals(GameManagerData.Level.Level1))
            {
                if (level1Upgrade)
                {
                    level1Upgrade.SetActive(true);
                    StartCoroutine(Fade(level1Upgrade, 0f, 1f, fadeInDuration));
                }
            }

            if (gameManagerData.level.Equals(GameManagerData.Level.Level2))
            {
                if (level1Upgrade)
                {
                    level1Upgrade.SetActive(true);
                    StartCoroutine(Fade(level1Upgrade, 0f, 1f, fadeInDuration));
                }
                if (level2Upgrade)
                {
                    level2Upgrade.SetActive(true);
                    StartCoroutine(Fade(level2Upgrade, 0f, 1f, fadeInDuration));
                }
            }

            if (gameManagerData.level.Equals(GameManagerData.Level.Level3))
            {
                if (level1Upgrade)
                {
                    level1Upgrade.SetActive(true);
                    StartCoroutine(Fade(level1Upgrade, 0f, 1f, fadeInDuration));
                }
                if (level2Upgrade)
                {
                    level2Upgrade.SetActive(true);
                    StartCoroutine(Fade(level2Upgrade, 0f, 1f, fadeInDuration));
                }
                if (level3Upgrade)
                {
                    level3Upgrade.SetActive(true);
                    StartCoroutine(Fade(level3Upgrade, 0f, 1f, fadeInDuration));
                }
            }
            

            StartCoroutine(Fade(bulletFactory, 0f, 1f, fadeInDuration));
            StartCoroutine(Fade(powerFactory, 0f, 1f, fadeInDuration));
            StartCoroutine(Fade(shieldFactory, 0f, 1f, fadeInDuration));
            StartCoroutine(Fade(healthFactory, 0f, 1f, fadeInDuration));
            StartCoroutine(Fade(darkenBackground, 0f, darkenBackgroundAlpha, fadeInDuration));
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            isUp = false;
            if (AudioManager.Instance)
                AudioManager.Instance.IncreaseAudioSpeed();
            
            if (networkObjective)
            {
                networkObjective.SetActive(true);
                StartCoroutine(Fade(networkObjective, 1f, 0f, fadeOutDuration));
            }

            if (healthObjective)
            {
                healthObjective.SetActive(true);
                StartCoroutine(Fade(healthObjective, 1f, 0f, fadeOutDuration));
            }
            
            if (level1EnemyObjective)
            {
                level1EnemyObjective.SetActive(true);
                StartCoroutine(Fade(level1EnemyObjective, 1f, 0f, fadeOutDuration));
            }
            
            if (level2EnemyObjective)
            {
                level2EnemyObjective.SetActive(true);
                StartCoroutine(Fade(level2EnemyObjective, 1f, 0f, fadeOutDuration));
            }
            
            if (level3EnemyObjective)
            {
                level3EnemyObjective.SetActive(true);
                StartCoroutine(Fade(level3EnemyObjective, 1f, 0f, fadeOutDuration));
            }
            
            if (gameManagerData.level.Equals(GameManagerData.Level.Level1))
            {
                if (level1Upgrade)
                {
                    level1Upgrade.SetActive(true);
                    StartCoroutine(Fade(level1Upgrade, 1f, 0f, fadeOutDuration));
                }
            }

            if (gameManagerData.level.Equals(GameManagerData.Level.Level2))
            {
                if (level1Upgrade)
                {
                    level1Upgrade.SetActive(true);
                    StartCoroutine(Fade(level1Upgrade, 1f, 0f, fadeOutDuration));
                }
                if (level2Upgrade)
                {
                    level2Upgrade.SetActive(true);
                    StartCoroutine(Fade(level2Upgrade, 1f, 0f, fadeOutDuration));
                }
            }

            if (gameManagerData.level.Equals(GameManagerData.Level.Level3))
            {
                if (level1Upgrade)
                {
                    level1Upgrade.SetActive(true);
                    StartCoroutine(Fade(level1Upgrade, 1f, 0f, fadeOutDuration));
                }
                if (level2Upgrade)
                {
                    level2Upgrade.SetActive(true);
                    StartCoroutine(Fade(level2Upgrade, 1f, 0f, fadeOutDuration));
                }
                if (level3Upgrade)
                {
                    level3Upgrade.SetActive(true);
                    StartCoroutine(Fade(level3Upgrade, 1f, 0f, fadeOutDuration));
                }
            }

            StartCoroutine(Fade(bulletFactory, 1f, 0f, fadeOutDuration));
            StartCoroutine(Fade(powerFactory, 1f, 0f, fadeOutDuration));
            StartCoroutine(Fade(shieldFactory, 1f, 0f, fadeOutDuration));
            StartCoroutine(Fade(healthFactory, 1f, 0f, fadeOutDuration));
            StartCoroutine(Fade(darkenBackground, darkenBackgroundAlpha, 0f, fadeOutDuration));
        }
    }
}
