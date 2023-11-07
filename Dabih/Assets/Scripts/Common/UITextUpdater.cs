using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextUpdater : MonoBehaviour
{
    [SerializeField] bool fades;
    [SerializeField] float fadeTimer = 0.5f;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    public void SetText(Component sender, object data)
    {
        if (data is string)
        {
            textMeshProUGUI.text = (string) data;
        }

        if (fades)
        {
            StartCoroutine(FadeTextToZeroAlpha());
        }
    }

    public IEnumerator FadeTextToZeroAlpha()
    {
        textMeshProUGUI.color = new Color(textMeshProUGUI.color.r, textMeshProUGUI.color.g, textMeshProUGUI.color.b, 1);
        while (textMeshProUGUI.color.a > 0.0f)
        {
            textMeshProUGUI.color = new Color(textMeshProUGUI.color.r, textMeshProUGUI.color.g, textMeshProUGUI.color.b, textMeshProUGUI.color.a - (Time.deltaTime / fadeTimer));
            yield return null;
        }
    }
}