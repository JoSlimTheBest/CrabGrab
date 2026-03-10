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
    [SerializeField] private float startDelay = 0f;
    [SerializeField] private float delayBeforeStart = 1f;
    [SerializeField] private float restAfterCycle = 3f;
    [SerializeField] private bool loop = true;

    private Coroutine trapCoroutine;

    private void OnEnable()
    {
        if (trapObject != null)
            trapObject.SetActive(false);

        if (warningLight != null)
            warningLight.SetActive(false);

        trapCoroutine = StartCoroutine(StartTrap());
    }

    private IEnumerator StartTrap()
    {
        if (startDelay > 0)
            yield return new WaitForSeconds(startDelay);

        yield return StartCoroutine(TrapRoutine());
    }

    private IEnumerator TrapRoutine()
    {
        do
        {
            if (warningLight != null)
                warningLight.SetActive(true);

            yield return new WaitForSeconds(delayBeforeStart);

            for (int i = 0; i < attackCount; i++)
            {
                if (trapObject != null)
                    trapObject.SetActive(true);

                yield return new WaitForSeconds(attackDuration);

                if (trapObject != null)
                    trapObject.SetActive(false);

                if (i < attackCount - 1)
                    yield return new WaitForSeconds(intervalBetweenAttacks);
            }

            if (warningLight != null)
                warningLight.SetActive(false);

            yield return new WaitForSeconds(restAfterCycle);

        } while (loop);
    }

    public void StopTrap()
    {
        if (trapCoroutine != null)
        {
            StopCoroutine(trapCoroutine);
            trapCoroutine = null;
        }

        if (trapObject != null)
            trapObject.SetActive(false);

        if (warningLight != null)
            warningLight.SetActive(false);
    }
}