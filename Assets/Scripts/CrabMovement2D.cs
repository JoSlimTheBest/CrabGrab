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
    [SerializeField] private bool startLocked = true; // стартовая блокировка

    private Rigidbody2D _rb;
    private Vector2 _input;

    [Header("Touch / Mouse")]
    [SerializeField] private float maxTouchDistance = 100f; // радиус "виртуального джойстика"
    private Vector2 startTouchPos;
    private int touchId = -1;

    public bool canMove = false;

    public AudioClip[] stepClips;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        canMove = !startLocked; // если startLocked = true → canMove = false
        Invoke("OpenMove", 4f);
    }

    public void OpenMove()
    {
        canMove = true;
    }

    private void Update()
    {
        if (!canMove)
        {
            _input = Vector2.zero;

            if (animator != null)
                animator.SetBool("isMoving", false);

            return;
        }

        // 📱 Сначала проверяем тач/мышь
        HandleTouchInput();

        // 🖥 Если тач/мышь нет — используем клавиатуру
        if (_input == Vector2.zero)
            HandleKeyboardInput();

        // Анимация
        bool isMoving = _input.sqrMagnitude > 0.01f;

        if (animator != null)
            animator.SetBool("isMoving", isMoving);
    }

    private void HandleKeyboardInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _input = new Vector2(moveX, moveY);

        if (normalizeDiagonal && _input.magnitude > 1f)
            _input = _input.normalized;
    }

    private void HandleTouchInput()
    {
        // 🖱 Мышь
        if (Input.mousePresent)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTouchPos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 delta = (Vector2)Input.mousePosition - startTouchPos;
                _input = Vector2.ClampMagnitude(delta / maxTouchDistance, 1f);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _input = Vector2.zero;
            }
        }

        // 📱 Тач
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began && touchId == -1)
                {
                    touchId = touch.fingerId;
                    startTouchPos = touch.position;
                }

                if (touch.fingerId == touchId)
                {
                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        Vector2 delta = touch.position - startTouchPos;
                        _input = Vector2.ClampMagnitude(delta / maxTouchDistance, 1f);
                    }

                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        _input = Vector2.zero;
                        touchId = -1;
                    }
                }
            }
        }

        if (normalizeDiagonal && _input.magnitude > 1f)
            _input = _input.normalized;
    }

    private void FixedUpdate()
    {
        if (!canMove)
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
        if (stepClips.Length == 0) return;

        GetComponent<AudioSource>().PlayOneShot(stepClips[Random.Range(0, stepClips.Length)]);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;

        if (!canMove && animator != null)
            animator.SetBool("isMoving", false);
    }
}