using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CrabDiggingOut : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] private Image buttonA;
    [SerializeField] private Image buttonD;

    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = Color.gray;

    [Header("Crab Visual")]
    [SerializeField] private SpriteRenderer crabImage;
    [SerializeField] private List<Sprite> crabSprites;

    [SerializeField] private int framesPerPress = 3;
    [SerializeField] private float frameDelay = 0.1f;

    [Header("Pulse")]
    [SerializeField] private float pulseSpeed = 5f;
    [SerializeField] private float pulseAmount = 0.2f;

    [Header("Shake")]
    [SerializeField] private Transform shakeTarget;
    [SerializeField] private float shakeStrength = 5f;
    [SerializeField] private float shakeDuration = 0.1f;

    [Header("Animation End")]
    [SerializeField] private Animator animator;
    [SerializeField] private string finishTrigger = "Finish";

    [Header("Audio")]
    public AudioClip[] clips;
    public AudioSource audioSource;

    private bool expectA = true;
    private bool isAnimatingFrames = false;
    private bool isFinished = false;

    private int currentSpriteIndex = 0;
    private Vector3 originalShakePos;

    private void Awake()
    {
        if (shakeTarget != null)
            originalShakePos = shakeTarget.localPosition;

        UpdateButtonUI();
        UpdateCrabSprite();
    }

    private void Update()
    {
        if (isFinished)
            return;

        HandleInput();
        UpdateButtonUI();
        PulseActiveButton();
    }

    void HandleInput()
    {
        if (isAnimatingFrames) return; // ⛔ блок во время "анимации"

        bool pressedA = Input.GetKeyDown(KeyCode.A);
        bool pressedD = Input.GetKeyDown(KeyCode.D);

        if (!pressedA && !pressedD)
            return;

        if ((expectA && pressedA) || (!expectA && pressedD))
        {
            expectA = !expectA;

            // 🔊 звук
            if (audioSource != null && clips.Length > 0)
                audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);

            // 📳 тряска
            StartCoroutine(Shake());

            // 🎬 проигрываем 3 кадра
            StartCoroutine(PlayFrames());
        }
    }

    IEnumerator PlayFrames()
    {
        isAnimatingFrames = true;

        for (int i = 0; i < framesPerPress; i++)
        {
            currentSpriteIndex++;

            if (currentSpriteIndex >= crabSprites.Count)
            {
                FinishSequence();
                yield break;
            }

            UpdateCrabSprite();
            yield return new WaitForSeconds(frameDelay);
        }

        isAnimatingFrames = false;
    }

    void UpdateCrabSprite()
    {
        if (crabImage != null && crabSprites.Count > 0)
        {
            crabImage.sprite = crabSprites[currentSpriteIndex];
        }
    }

    void UpdateButtonUI()
    {
        if (buttonA != null)
            buttonA.color = expectA ? activeColor : inactiveColor;

        if (buttonD != null)
            buttonD.color = expectA ? inactiveColor : activeColor;
    }

    void PulseActiveButton()
    {
        float scale = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

        if (expectA && buttonA != null)
            buttonA.rectTransform.localScale = Vector3.one * scale;
        else if (buttonA != null)
            buttonA.rectTransform.localScale = Vector3.one;

        if (!expectA && buttonD != null)
            buttonD.rectTransform.localScale = Vector3.one * scale;
        else if (buttonD != null)
            buttonD.rectTransform.localScale = Vector3.one;
    }

    IEnumerator Shake()
    {
        if (shakeTarget == null)
            yield break;

        float t = 0;

        while (t < shakeDuration)
        {
            t += Time.deltaTime;

            Vector3 offset = Random.insideUnitSphere * shakeStrength;
            offset.z = 0;

            shakeTarget.localPosition = originalShakePos + offset;

            yield return null;
        }

        shakeTarget.localPosition = originalShakePos;
    }

    void FinishSequence()
    {


        crabImage.GetComponent<CrabMovement2D>().canMove = true; // разблокируем движение краба
        isFinished = true;

        // ⛔ скрываем кнопки
        if (buttonA != null) buttonA.gameObject.SetActive(false);
        if (buttonD != null) buttonD.gameObject.SetActive(false);

        // 🎬 запускаем финальную анимацию
       
        if (animator != null)
            animator.enabled = true;

        // 💥 удаляем объект через небольшую задержку
        Destroy(gameObject, 2f);
    }
}