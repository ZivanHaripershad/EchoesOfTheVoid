using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereReaction : MonoBehaviour
{
    [SerializeField]
    public GameObject bulletFactory;

    [SerializeField]
    public GameObject powerFactory;

    [SerializeField]
    public GameObject shieldFactory;

    [SerializeField]
    public GameObject healthFactory;

    public OrbDepositingMode orbDepositingMode;

    public float fadeDuration = 1f; // The duration of the fade-in effect

    // Start is called before the first frame update
    void Start()
    {
        bulletFactory.SetActive(false);
        powerFactory.SetActive(false);
        shieldFactory.SetActive(false);
        healthFactory.SetActive(false);

    }

    IEnumerator Fade(GameObject gameObject, float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0f;
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();

        // Gradually fade the renderers
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);

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
        if (Input.GetKeyDown(KeyCode.S) && orbDepositingMode.depositingMode)
        {
            bulletFactory.SetActive(true);
            powerFactory.SetActive(true);
            shieldFactory.SetActive(true);
            healthFactory.SetActive(true);

            StartCoroutine(Fade(bulletFactory, 0f, 1f));
            StartCoroutine(Fade(powerFactory, 0f, 1f));
            StartCoroutine(Fade(shieldFactory, 0f, 1f));
            StartCoroutine(Fade(healthFactory, 0f, 1f));
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            StartCoroutine(Fade(bulletFactory, 1f, 0f));
            StartCoroutine(Fade(powerFactory, 1f, 0f));
            StartCoroutine(Fade(shieldFactory, 1f, 0f));
            StartCoroutine(Fade(healthFactory, 1f, 0f));
        }
    }
}
