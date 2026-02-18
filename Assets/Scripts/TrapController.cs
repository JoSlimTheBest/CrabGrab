using System.Collections;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject warningLight;
    [SerializeField] private GameObject trapObject;

    [Header("Attack Settings")]
    [SerializeField] private float attackDuration = 0.5f;
    [SerializeField] private float intervalBetweenAttacks = 1f;
    [SerializeField] private int attackCount = 3;

    [Header("Cycle Settings")]
    [SerializeField] private float delayBeforeStart = 1f;   // ВРЕМЯ СВЕЧЕНИЯ ДО ПЕРВОГО УДАРА
    [SerializeField] private float restAfterCycle = 3f;
    [SerializeField] private bool loop = true;

    private void OnEnable()
    {
        if (trapObject != null)
            trapObject.SetActive(false);

        if (warningLight != null)
            warningLight.SetActive(false);

        StartCoroutine(TrapRoutine());
    }

    private IEnumerator TrapRoutine()
    {
        do
        {
            // 🔦 Включаем свет
            if (warningLight != null)
                warningLight.SetActive(true);

            // ⏳ Свет горит ДО первого удара
            yield return new WaitForSeconds(delayBeforeStart);

            for (int i = 0; i < attackCount; i++)
            {
                // 💥 Включаем ловушку
                if (trapObject != null)
                    trapObject.SetActive(true);

                yield return new WaitForSeconds(attackDuration);

                // 🔕 Выключаем ловушку
                if (trapObject != null)
                    trapObject.SetActive(false);

                if (i < attackCount - 1)
                    yield return new WaitForSeconds(intervalBetweenAttacks);
            }

            // 🔦 Выключаем свет после серии
            if (warningLight != null)
                warningLight.SetActive(false);

            yield return new WaitForSeconds(restAfterCycle);

        } while (loop);
    }
}
