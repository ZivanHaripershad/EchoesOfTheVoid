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

    [SerializeField] private BulletDeposit bulletDeposit;
    [SerializeField] private HealthDeposit healthDeposit;
    [SerializeField] private PowerDeposit powerDeposit;
    [SerializeField] private ShieldDeposit shieldDeposit;
    [SerializeField] private GameObject darkenBackground;
    [SerializeField] private float darkenBackgroundAlpha;

    public OrbDepositingMode orbDepositingMode;

    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDuration;
    
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
        if (Input.GetKey(KeyCode.S) && orbDepositingMode.depositingMode)
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

            StartCoroutine(Fade(bulletFactory, 0f, 1f, fadeInDuration));
            StartCoroutine(Fade(powerFactory, 0f, 1f, fadeInDuration));
            StartCoroutine(Fade(shieldFactory, 0f, 1f, fadeInDuration));
            StartCoroutine(Fade(healthFactory, 0f, 1f, fadeInDuration));
            StartCoroutine(Fade(darkenBackground, 0f, darkenBackgroundAlpha, fadeInDuration));
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isUp = false;
            if (AudioManager.Instance)
                AudioManager.Instance.IncreaseAudioSpeed();
            StartCoroutine(Fade(bulletFactory, 1f, 0f, fadeOutDuration));
            StartCoroutine(Fade(powerFactory, 1f, 0f, fadeOutDuration));
            StartCoroutine(Fade(shieldFactory, 1f, 0f, fadeOutDuration));
            StartCoroutine(Fade(healthFactory, 1f, 0f, fadeOutDuration));
            StartCoroutine(Fade(darkenBackground, darkenBackgroundAlpha, 0f, fadeOutDuration));
        }
    }
}
