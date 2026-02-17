using System.Collections;
using UnityEngine;

namespace Game.Scripts.Core.Player.View
{
    public class PlayerView : MonoBehaviour
    {
        [Header("Physics")]
        [SerializeField] private Rigidbody2D _rb;

        [Header("Animation")]
        [SerializeField] private Animator _animator;
        [SerializeField] private string _moveParameter = "IsMoving";

        [Header("Footsteps")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _footstepClips;

        private bool _isMoving;

        [Header("Movement Speed")]
        [SerializeField] private float horizontalSpeed = 1f; // скорость влево/вправо
        [SerializeField] private float verticalSpeed = 1f;   // скорость вперед/назад

        private void OnValidate()
        {
            if (_rb == null)
                _rb = GetComponent<Rigidbody2D>();

            if (_animator == null)
                _animator = GetComponent<Animator>();

            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();
        }

        #region Movement

        public void MoveByVelocity(Vector2 velocity, float deltaTime)
        {
            if (_rb == null)
                return;

            // Применяем разные скорости по осям
            float moveX = velocity.x * horizontalSpeed;
            float moveY = velocity.y * verticalSpeed;

            Vector2 move = new Vector2(moveX, moveY);
            Vector2 target = _rb.position + move * deltaTime;

            _rb.MovePosition(target);
        }

        #endregion

        #region Animation Control (ВАЖНО)

        public void SetMoving(bool value)
        {
            if (_isMoving == value)
                return;

            _isMoving = value;

            if (_animator != null)
                _animator.SetBool(_moveParameter, _isMoving);
        }

        #endregion

        #region Footsteps (Animation Event)

        public void PlayFootstep()
        {
            if (!_isMoving)
                return;

            if (_audioSource == null || _footstepClips == null || _footstepClips.Length == 0)
                return;

            var clip = _footstepClips[Random.Range(0, _footstepClips.Length)];
            _audioSource.PlayOneShot(clip);
        }

       
        #endregion

        #region SmoothMove

        public Coroutine SmoothMoveTo(Transform target, float duration)
        {
            if (target == null)
                return null;

            return SmoothMoveTo(target.position, duration);
        }

        public Coroutine SmoothMoveTo(Vector3 targetPosition, float duration)
        {
            return StartCoroutine(SmoothMoveRoutine(targetPosition, duration));
        }

        private IEnumerator SmoothMoveRoutine(Vector3 targetPosition, float duration)
        {
            var start = transform.position;
            var elapsed = 0f;

            SetMoving(true);

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / duration);
                transform.position = Vector3.Lerp(start, targetPosition, t);
                yield return null;
            }

            transform.position = targetPosition;

            SetMoving(false);
        }

        #endregion
    }
}
