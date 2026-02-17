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
        }
    }
}