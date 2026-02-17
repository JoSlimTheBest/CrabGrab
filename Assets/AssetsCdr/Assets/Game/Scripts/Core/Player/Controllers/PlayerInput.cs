using System;
using UnityEngine;
using Game.Scripts.Core;

namespace Game.Scripts.Core.Player.Controllers
{
    public class PlayerInput
    {
        public event Action<Vector2> OnInput;

        private bool _enabled = true;
        private readonly GameSettings _settings;

        public PlayerInput(GameSettings settings = null)
        {
            _settings = settings;
        }

        public bool Enabled
        {
            get => _enabled;
            set => _enabled = value;
        }

        public Vector2 GetMoveInput()
        {
            if (!_enabled)
                return Vector2.zero;

            // Using old Input Manager axes for simplicity
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector2 dir = new Vector2(x, y);
            if (dir.sqrMagnitude > 1f)
                dir.Normalize();

            float speed = _settings != null ? _settings.PlayerMoveSpeed : 5f;
            Vector2 velocity = dir * speed;

            OnInput?.Invoke(velocity);
            return velocity;
        }
    }
}