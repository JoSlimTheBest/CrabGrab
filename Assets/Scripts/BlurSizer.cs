using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurSizer : MonoBehaviour
{
    public Material blurMaterial; // Материал с шейдером
    public float startValue = 0f; // Начальное значение Blur
    public float maxValue = 0.01f; // Максимальное значение Blur
    public float step = 0.001f; // Шаг при каждом FixedUpdate

    private float currentValue;

    public GameObject text;

    public List<AudioClip> audioClips; // Список аудиоклипов для воспроизведения

    private void OnEnable()
    {
        currentValue = startValue;
        if (blurMaterial != null)
            blurMaterial.SetFloat("_BlurSize", currentValue);
        text.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
    }

    private void FixedUpdate()
    {
        if (blurMaterial == null) return;

        if (currentValue < maxValue)
        {
            currentValue += step;
            if (currentValue > maxValue)
                currentValue = maxValue;

            blurMaterial.SetFloat("_BlurSize", currentValue);
        }
        else
        {
            text.SetActive(true);
        }
    }
}
