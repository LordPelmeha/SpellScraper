using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public float fadeDuration;
    private Image fadeImage;

    void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    public IEnumerator FadeIn()
    {
        while (fadeImage.color.a < 1)
        {
            Color color = fadeImage.color;
            color.a += Time.deltaTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }
    }
    public IEnumerator Inlight()
    {
        while (fadeImage.color.a >0)
        {
            Color color = fadeImage.color;
            color.a -= Time.deltaTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }
    }
}
