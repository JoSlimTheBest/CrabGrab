using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class RapidPressBar : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image fillImage;

    [Header("Fade")]
    [SerializeField] private Image fadeImage;     // экранный Image
    [SerializeField] private float fadeTime = 2f; // врем€ перехода

    [Header("Settings")]
    [SerializeField] private float pressAddPercent = 15f;
    [SerializeField] private float decayPerSecond = 3f;
    [SerializeField] private float maxPercent = 100f;

    private float currentPercent;

    public AudioClip[] clips;
    public AudioSource audioSource;

    private bool isTransition = false;

    private void Awake()
    {
       

        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0;
            fadeImage.color = c;
        }
    }

    private void Update()
    {
        if (isTransition)
            return;

        HandleInput();
        DecayBar();
        UpdateUI();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentPercent += pressAddPercent;
            currentPercent = Mathf.Clamp(currentPercent, 0, maxPercent);

            if (currentPercent >= maxPercent)
            {
                StartCoroutine(NextLVL());
                currentPercent = 0;
            }

            audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }

    void DecayBar()
    {
        currentPercent -= decayPerSecond * Time.deltaTime;
        currentPercent = Mathf.Clamp(currentPercent, 0, maxPercent);
    }

    void UpdateUI()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = currentPercent / maxPercent;
        }
    }

    IEnumerator NextLVL()
    {
        isTransition = true;

        fillImage.gameObject.SetActive(false);

        float t = 0;
        Color c = fadeImage.color;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float progress = t / fadeTime;

            // Alpha увеличиваетс€
            c.a = progress;

            // ÷вет мен€етс€ от чЄрного к белому
            c.r = progress;
            c.g = progress;
            c.b = progress;

            fadeImage.color = c;

            // затухание музыки
            audioSource.volume = 1f - progress;

            yield return null;
        }

        SceneManager.LoadScene(1);
    }
}