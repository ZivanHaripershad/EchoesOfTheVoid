using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AtmosphereReactionLevel1 : MonoBehaviour
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

    public Level1Controller level1Controller;

    public OrbDepositingMode orbDepositingMode;

    public float fadeDuration = 1f; // The duration of the fade-in effect

    IEnumerator Fade(GameObject gameObject, float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0f;
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();

        // Gradually fade the renderers
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration); ;
            
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

    void Update()
    {
        if (Input.GetKey(KeyCode.S) && orbDepositingMode.depositingMode)
        {
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

            StartCoroutine(Fade(bulletFactory, 0f, 1f));
            StartCoroutine(Fade(powerFactory, 0f, 1f));
            StartCoroutine(Fade(shieldFactory, 0f, 1f));
            StartCoroutine(Fade(healthFactory, 0f, 1f));
            StartCoroutine(Fade(darkenBackground, 0f, darkenBackgroundAlpha));
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            AudioManager.Instance.IncreaseAudioSpeed();
            StartCoroutine(Fade(bulletFactory, 1f, 0f));
            StartCoroutine(Fade(powerFactory, 1f, 0f));
            StartCoroutine(Fade(shieldFactory, 1f, 0f));
            StartCoroutine(Fade(healthFactory, 1f, 0f));
            StartCoroutine(Fade(darkenBackground, darkenBackgroundAlpha, 0f));
        }
    }
}
