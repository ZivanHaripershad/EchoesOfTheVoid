using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UrgentMessage : MonoBehaviour
{
    
    private Text textComponent;
    private Coroutine flashingCoroutine;

    public float flashSpeed = 2.0f;
    public float minOpacity = 0.2f;
    public float maxOpacity = 1.0f;

    private void Start()
    {
        textComponent = GetComponent<Text>();
    }

    private void OnEnable()
    {
        if (flashingCoroutine != null)
            StopCoroutine(flashingCoroutine);

        flashingCoroutine = StartCoroutine(FlashText());
    }

    private void OnDisable()
    {
        if (flashingCoroutine != null)
            StopCoroutine(flashingCoroutine);
    }

    private IEnumerator FlashText()
    {
        Color originalColor = textComponent.color;

        while (true)
        {
            float t = Mathf.PingPong(Time.time * flashSpeed, 1);
            Color lerpedColor = Color.Lerp(originalColor, new Color(originalColor.r, originalColor.g, originalColor.b, minOpacity), t);
            textComponent.color = lerpedColor;
            yield return null;
        }
    }
}
