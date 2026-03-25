using Game.Scripts.Core.Player.View;

namespace Game.Scripts.Core.Player.Controllers
{
    public class PlayerController
    {
        private readonly PlayerInput _input;
        private readonly PlayerView _view;
        private readonly GameSettings _settings;

        public PlayerController(PlayerView view, GameSettings settings)
        {
            _view = view;
            _settings = settings;
            _input = new PlayerInput(_settings);
        }

        public void UpdatePhysics(float fixedDeltaTime)
        {
            var velocity = _input.GetMoveInput();

            // ❗ Не затираем движение тача
            if (velocity.sqrMagnitude > 0.001f)
            {
                _view.MoveByVelocity(velocity, fixedDeltaTime);
            }

            _view.SetMoving(velocity.sqrMagnitude > 0.001f);
        }

        public void SetInputEnabled(bool enabled)
        {
            _input.Enabled = enabled;
        }
    }
}