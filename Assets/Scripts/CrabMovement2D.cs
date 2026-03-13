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

    [Header("Start Lock")]
    [SerializeField] private float startLockTime = 3f;

    private Rigidbody2D _rb;
    private Vector2 _input;

    private float lockTimer;

    public AudioClip[] stepClips;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        lockTimer = startLockTime;
    }

    private void Update()
    {
        // ⛔ Блокировка управления
        if (lockTimer > 0f)
        {
            lockTimer -= Time.deltaTime;
            _input = Vector2.zero;

            if (animator != null)
                animator.SetBool("isMoving", false);

            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _input = new Vector2(moveX, moveY);

        if (normalizeDiagonal && _input.magnitude > 1f)
            _input = _input.normalized;

        bool isMoving = _input.sqrMagnitude > 0.01f;

        if (animator != null)
            animator.SetBool("isMoving", isMoving);
    }

    private void FixedUpdate()
    {
        if (lockTimer > 0f)
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        Vector2 velocity = new Vector2(
            _input.x * speedX,
            _input.y * speedY
        );

        _rb.velocity = velocity;
    }

    public void AudioStep()
    {
        GetComponent<AudioSource>().PlayOneShot(stepClips[Random.Range(0, stepClips.Length)]);
    }
}