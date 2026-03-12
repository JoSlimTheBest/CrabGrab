using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RapidPressBar : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image fillImage; // Картинка круга

    [Header("Settings")]
    [SerializeField] private float pressAddPercent = 15f; // сколько % даёт нажатие
    [SerializeField] private float decayPerSecond = 3f;   // сколько % теряется в секунду
    [SerializeField] private float maxPercent = 100f;

    private float currentPercent;


    public AudioClip[] clips;
    private void Update()
    {
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
                NextLVL();
                currentPercent = 0;
            }
            GetComponent<AudioSource>().PlayOneShot(clips[Random.Range(0, clips.Length)]);
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

    void NextLVL()
    {
        fillImage.gameObject.SetActive(false);

        SceneManager.LoadScene(2);
    }
}