using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignWay : MonoBehaviour
{
    public void AnimationLevel(int currentLvl, Transform startPoint)
    {
        // Ставим объект на стартовую позицию уровня
        if (startPoint != null)
            transform.position = startPoint.position;

        // Включаем объект
        gameObject.SetActive(true);

        // Получаем Animator
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            // Сразу проигрываем анимацию уровня, минуя Idle
            anim.Play($"RunLevel{currentLvl}", -1, 0f);

            // Если нужен триггер для переходов, можно также SetTrigger
            // anim.SetTrigger($"RunLevel{currentLvl}");
        }
    }


    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
