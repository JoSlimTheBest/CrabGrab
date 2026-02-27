using UnityEngine;

public class CameraFollowX : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float offsetX = 0f;

    private void FixedUpdate()
    {
        if (target == null)
            return;

        float targetX = target.position.x + offsetX;

        Vector3 newPosition = new Vector3(
            Mathf.Lerp(transform.position.x, targetX, smoothSpeed * Time.deltaTime),
            transform.position.y,
            transform.position.z
        );

        transform.position = newPosition;
    }
}
