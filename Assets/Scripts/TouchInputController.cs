using Game.Scripts.Core.Player.View;
using UnityEngine;

public class TouchInputController : MonoBehaviour
{
    [Header("Links")]
    public PlayerView player;

    [Header("Settings")]
    public float maxDistance = 100f; // радиус "джойстика"

    private Vector2 startTouchPos;
    private Vector2 currentInput;
    private int touchId = -1;

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
           
             //   Debug.Log("Move: " + currentInput);



            player.MoveByVelocity(currentInput.normalized*3.3f, Time.fixedDeltaTime);
           
        }
    }

    void HandleInput()
    {
        // 📱 Если есть тач — используем его
        if (Input.touchCount > 0)
        {
            HandleTouch();
        }
        else
        {
            HandleMouse();
        }
    }

    #region TOUCH
    void HandleTouch()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Начало касания
            if (touch.phase == TouchPhase.Began && touchId == -1)
            {
                touchId = touch.fingerId;
                startTouchPos = touch.position;
            }

            // Работаем только с одним пальцем
            if (touch.fingerId == touchId)
            {
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    Vector2 delta = touch.position - startTouchPos;

                    currentInput = Vector2.ClampMagnitude(delta / maxDistance, 1f);
                }

                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    ResetInput();
                }
            }
        }
    }
    #endregion

    #region MOUSE
    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)Input.mousePosition - startTouchPos;

            currentInput = Vector2.ClampMagnitude(delta / maxDistance, 1f);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ResetInput();
        }
    }
    #endregion

    void ResetInput()
    {
        currentInput = Vector2.zero;
        touchId = -1;
    }
}