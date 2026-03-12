using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CrabMovement2D : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float speedX = 5f;
    [SerializeField] private float speedY = 3f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Options")]
    [SerializeField] private bool normalizeDiagonal = true;

    private Rigidbody2D _rb;
    private Vector2 _input;


    public AudioClip[] stepClips; // Звуковой эффект шага

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _input = new Vector2(moveX, moveY);

        if (normalizeDiagonal && _input.magnitude > 1f)
            _input = _input.normalized;

        // 🎬 Проверяем движение
        bool isMoving = _input.sqrMagnitude > 0.01f;

        if (animator != null)
            animator.SetBool("isMoving", isMoving);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = new Vector2(
            _input.x * speedX,
            _input.y * speedY
        );

        _rb.velocity = velocity;
    }


    public void AudioStep()
    {
        // Этот метод вызывается из анимации шага
        // Здесь можно добавить код для воспроизведения звука шага
        // Например:
         GetComponent<AudioSource>().PlayOneShot(stepClips[Random.Range(0, stepClips.Length)]);
    }
}