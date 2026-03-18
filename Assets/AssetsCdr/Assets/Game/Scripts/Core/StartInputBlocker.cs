using Game.Scripts.Core;
using UnityEngine;

public class StartInputBlocker : MonoBehaviour
{
    [SerializeField] private float disableTime = 5f;
    [SerializeField] private float delayBeforeBlock = 0.3f;

    void Start()
    {
        // Ждём пока все системы (ChangeLevel и т.д.) включат ввод
        Invoke(nameof(DisableInput), delayBeforeBlock);
    }

    void DisableInput()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }

        // 🔒 Отключаем управление
        GameManager.Instance.PlayerController.SetInputEnabled(false);

        // 🔓 Запланировать включение обратно
        Invoke(nameof(EnableInput), disableTime);
    }

    void EnableInput()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerController.SetInputEnabled(true);
        }
    }
}