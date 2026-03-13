using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTime = 2f;

    private void Start()
    {
        StartCoroutine(FadeFromWhite());
    }

    IEnumerator FadeFromWhite()
    {
        float t = 0;

        Color c = fadeImage.color;

        // стартуем с белого
        c.r = 1;
        c.g = 1;
        c.b = 1;
        c.a = 1;

        fadeImage.color = c;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            float progress = t / fadeTime;

            // убираем белый
            c.a = 1 - progress;

            fadeImage.color = c;

            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }
}