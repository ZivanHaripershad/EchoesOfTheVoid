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

    private bool isEnabled;

    private void Start()
    {
        isEnabled = true;
        textComponent = GetComponent<Text>();
        Hide();
        flashingCoroutine = StartCoroutine(FlashText());
    }

    public void Hide()
    {
        if (isEnabled)
        {
            isEnabled = false;
            textComponent.color = new Color(188f / 255f, 66f / 255f, 66f / 255f, 0f);
        }
    }

    public void Show()
    {
        if (!isEnabled)
        {
            isEnabled = true;
            textComponent.color = new Color(188f / 255f, 66f / 255f, 66f / 255f, 1f);
        }
    }
    private IEnumerator FlashText()
    {
        Color originalColor = textComponent.color;

        while (isEnabled)
        {
            float t = Mathf.PingPong(Time.time * flashSpeed, 1);
            Color lerpedColor = Color.Lerp(originalColor, new Color(originalColor.r, originalColor.g, originalColor.b, minOpacity), t);
            textComponent.color = lerpedColor;
            yield return null;
        }
    }
}
