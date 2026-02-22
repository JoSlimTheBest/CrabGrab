using UnityEngine;

public class KillButtons : MonoBehaviour
{
    [Header("Cube Controllers")]
    [SerializeField] private CubeController cubeW;
    [SerializeField] private CubeController cubeA;
    [SerializeField] private CubeController cubeS;
    [SerializeField] private CubeController cubeD;

    private bool usedW, usedA, usedS, usedD;
    private bool deactivatedW, deactivatedA, deactivatedS, deactivatedD;

    private bool allActivated = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            HandleInput(KeyCode.W, cubeW, ref usedW, ref deactivatedW);

        if (Input.GetKeyDown(KeyCode.A))
            HandleInput(KeyCode.A, cubeA, ref usedA, ref deactivatedA);

        if (Input.GetKeyDown(KeyCode.S))
            HandleInput(KeyCode.S, cubeS, ref usedS, ref deactivatedS);

        if (Input.GetKeyDown(KeyCode.D))
            HandleInput(KeyCode.D, cubeD, ref usedD, ref deactivatedD);
    }

    private void HandleInput(KeyCode key, CubeController cube, ref bool usedFlag, ref bool deactivatedFlag)
    {
        // Первая фаза — активация
        if (!allActivated)
        {
            cube.ActivateCube();
            usedFlag = true;

            if (usedW && usedA && usedS && usedD)
            {
                allActivated = true;
                Debug.Log("Все кубы активированы. Ждём деактивации.");
            }
        }
        else
        {
            // Вторая фаза — деактивация
            if (!deactivatedFlag)
            {
                cube.DeActivateCube();
                deactivatedFlag = true;
            }

            // Проверяем, все ли деактивированы
            if (deactivatedW && deactivatedA && deactivatedS && deactivatedD)
            {
                Debug.Log("Все кубы деактивированы. Скрипт удаляется.");
               Invoke("DestroyObj",1f);
            }
        }
    }


    private void DestroyObj()
    {
        GetComponent<CanvasGroupFader>().FadeOut();
        
             Invoke("DestroyObjects", 3f); // Удаляем объекты после завершения анимации
    }

    private void DestroyObjects()
    {
        if (cubeW != null) Destroy(cubeW.gameObject);
        if (cubeA != null) Destroy(cubeA.gameObject);
        if (cubeS != null) Destroy(cubeS.gameObject);
        if (cubeD != null) Destroy(cubeD.gameObject);
        Destroy(gameObject);    
    }
}