using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCanvasOpen : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // объект который нужно открыть

    public float timing = 180f; // 3 минуты = 180 секунд

    private bool opened;

    public CrabMovement2D cd2d;

    void Update()
    {
        if (opened)
            return;

        if (timing > 0)
        {
            timing -= Time.deltaTime;
        }
        else
        {
            OpenObject();
        }
    }

    void OpenObject()
    {
        opened = true;

        if (targetObject != null)
            targetObject.SetActive(true);



         cd2d.SetCanMove (false); // блокируем движение краба
    }
}
