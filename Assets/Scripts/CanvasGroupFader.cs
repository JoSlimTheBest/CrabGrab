using System.Collections;
using UnityEngine;

public class CanvasGroupFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Start Settings")]
    [SerializeField] private float startDelay = 3f; // ⏱ Задержка перед первым появлением

    private Coroutine _currentFade;

    private void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Start()
    {
        StartCoroutine(DelayedFadeIn());
    }

    private IEnumerator DelayedFadeIn()
    {
        if (startDelay > 0)
            yield return new WaitForSeconds(startDelay);

        FadeIn();
    }

    public void FadeIn()
    {
        StartFade(1f); // ⚠️ У тебя было 2f — альфа должна быть максимум 1
    }

    public void FadeOut()
    {
        StartFade(0f);
    }

    private void StartFade(float targetAlpha)
    {
        if (_currentFade != null)
            StopCoroutine(_currentFade);

        _currentFade = StartCoroutine(Fade(targetAlpha));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        canvasGroup.interactable = targetAlpha > 0.9f;
        canvasGroup.blocksRaycasts = targetAlpha > 0.9f;
    }
}