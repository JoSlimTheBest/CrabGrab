using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageFollowCube : MonoBehaviour
{
    [Header("Target 3D Object")]
    [SerializeField] private Transform target;

    private RectTransform _rectTransform;
    private Camera _mainCamera;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Переводим позицию из мира в экранные координаты
        Vector3 screenPos = _mainCamera.WorldToScreenPoint(target.position);

        // Устанавливаем позицию UI
        _rectTransform.position = screenPos;

        // Копируем вращение по Z
        _rectTransform.rotation = Quaternion.Euler(0, 0, target.eulerAngles.z);
    }
}
