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

            // 1️⃣ Двигаем игрока каждый кадр, даже если скорость 0
            _view.MoveByVelocity(velocity, fixedDeltaTime);

            // 2️⃣ Устанавливаем флаг движения для анимации
            _view.SetMoving(velocity.sqrMagnitude > 0.001f);
        }

        public void SetInputEnabled(bool enabled)
        {
            _input.Enabled = enabled;
        }
    }
}