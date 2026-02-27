using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CrabMovement2D : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float speedX = 5f;
    [SerializeField] private float speedY = 3f;

    [Header("Options")]
    [SerializeField] private bool normalizeDiagonal = true;

    private Rigidbody2D _rb;
    private Vector2 _input;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D или ← →
        float moveY = Input.GetAxisRaw("Vertical");   // W/S или ↑ ↓

        _input = new Vector2(moveX, moveY);

        if (normalizeDiagonal && _input.magnitude > 1f)
            _input = _input.normalized;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = new Vector2(
            _input.x * speedX,
            _input.y * speedY
        );

        _rb.velocity = velocity;
    }
}