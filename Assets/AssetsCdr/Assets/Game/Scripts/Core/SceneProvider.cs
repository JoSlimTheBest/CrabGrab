using Game.Scripts.Core.Player.View;
using Game.Scripts.Core.Level.View;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class SceneProvider : MonoBehaviour
    {
        [SerializeField]
        private GameSettings _gameSettings;

        [SerializeField]
        private PlayerView _playerView;

        [SerializeField]
        private Transform _cameraTransform;

		[SerializeField]
		private Transform _levelsRoot;

        [SerializeField]
        private OneLevel[] _levels;
        
        public GameSettings GameSettings => _gameSettings;
        public PlayerView PlayerView => _playerView;
        public Transform CameraTransform => _cameraTransform;
        public OneLevel[] Levels => _levels;

        private void OnValidate()
        {
            if (_cameraTransform == null && Camera.main != null)
                _cameraTransform = Camera.main.transform;

			if (_levelsRoot != null)
				_levels = _levelsRoot.GetComponentsInChildren<OneLevel>(true);
        }

        public OneLevel GetCurrentLevel()
        {
            foreach (var level in _levels)
            {
                if (level.VisualRoot != null && level.VisualRoot.activeSelf)
                    return level;
            }

            return null;
        }

        public void RestartCurrentLevel()
        {
            var level = GetCurrentLevel();
            if (level == null) return;

            level.RestartLevel(_playerView.transform);
            GameManager.Instance?.SetInputEnabled(true);
        }

        public void SetLevel(int lvl)
        {
            if (_levels == null || _levels.Length == 0)
                return;

            if (lvl < 0 || lvl >= _levels.Length)
            {
                Debug.LogWarning($"Level index {lvl} is out of range.");
                return;
            }

            // Выключаем все уровни
            foreach (var level in _levels)
            {
                if (level.VisualRoot != null)
                    level.VisualRoot.SetActive(false);
            }

            // Включаем нужный
            var targetLevel = _levels[lvl];
            if (targetLevel.VisualRoot != null)
                targetLevel.VisualRoot.SetActive(true);

            // Рестарт уровня с позицией игрока
            targetLevel.RestartLevel(_playerView.transform);

            GameManager.Instance?.SetInputEnabled(true);
        }


        public void LoadNextLevel()
        {
            if (_levels == null || _levels.Length == 0)
                return;

            var current = GetCurrentLevel();
            if (current == null)
                return;

            int currentIndex = System.Array.IndexOf(_levels, current);
            if (currentIndex < 0)
                return;

            int nextIndex = currentIndex + 1;

            // если это последний уровень
            if (nextIndex >= _levels.Length)
            {
                Debug.Log("Last level reached.");
                return;
                // если хочешь зациклить:
                // nextIndex = 0;
            }

            SetLevel(nextIndex);
        }
    }
}