using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [Range(0f, 1f)]
    [SerializeField] private float parallaxFactor = 0.5f;

    private Vector3 _lastCameraPosition;

    private void Start()
    {
        _lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - _lastCameraPosition;

        transform.position += new Vector3(
            delta.x * parallaxFactor,
            delta.y * parallaxFactor,
            0f);

        _lastCameraPosition = cameraTransform.position;
    }
}