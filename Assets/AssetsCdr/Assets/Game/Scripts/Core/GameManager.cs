using Game.Scripts.Core.CameraFeature;
using Game.Scripts.Core.Level.View;
using Game.Scripts.Core.Player.Controllers;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class GameManager
    {
        private const float TransitionDuration = 0.5f;
        
        private readonly PlayerController _playerController;
        private readonly SceneProvider _sceneProvider;
        private readonly CameraController _cameraController;

        private OneLevel _currentLevel;
        private bool _isTransitioning;
        private float _transitionEndsAt;
        public static GameManager Instance { get; private set; }

        public GameManager(SceneProvider sceneProvider)
        {
            _sceneProvider = sceneProvider;
			_playerController = new PlayerController(_sceneProvider.PlayerView, _sceneProvider.GameSettings);
			Instance = this;

            _cameraController = new CameraController(_sceneProvider.CameraTransform);
            SubscribeToLevels();
        }
        
        public void Update()
        {
            var dt = Time.deltaTime;
            _cameraController?.Update(dt);

            if (!_isTransitioning || !(Time.time >= _transitionEndsAt))
                return;
            
            _isTransitioning = false;
            _playerController.SetInputEnabled(true);
        }

		public void FixedUpdate()
		{
			var fdt = Time.fixedDeltaTime;
			_playerController.UpdatePhysics(fdt);
		}

        public void SetInputEnabled(bool enabled, float resumeAfterSeconds = 0f)
        {
            _playerController.SetInputEnabled(enabled);
            if (enabled || !(resumeAfterSeconds > 0f))
                return;
            
            _isTransitioning = true;
            _transitionEndsAt = Time.time + resumeAfterSeconds;
        }

        private void SubscribeToLevels()
        {
            var levels = _sceneProvider.Levels;
            if (levels == null) 
                return;

            foreach (var level in levels)
            {
                if (level == null) 
                    continue;
                
                var captured = level;
                captured.onEnter += () => OnLevelEnter(captured);
            }
        }

        private void OnLevelEnter(OneLevel newLevel)
        {
            if (_isTransitioning || newLevel == _currentLevel)
                return;

            ChangeLevel(newLevel);
        }

        private void ChangeLevel(OneLevel newLevel)
        {
            if (_currentLevel != null)
            {
				_currentLevel.SetVisualEnabled(false);
                var prevCol = _currentLevel.LevelCollider;
                if (prevCol != null) 
                    prevCol.isTrigger = false;
            }

            _currentLevel = newLevel;

            _isTransitioning = true;
            _transitionEndsAt = Time.time + TransitionDuration;
            _playerController.SetInputEnabled(false);

            if (newLevel == null)
                return;
            
			newLevel.SetVisualEnabled(true);
			var newCol = newLevel.LevelCollider;
			if (newCol != null)
				newCol.isTrigger = true;
			FitCameraToLevel(newLevel);
            
            if (_sceneProvider.PlayerView != null && newLevel.StartPoint != null)
                _sceneProvider.PlayerView.SmoothMoveTo(newLevel.StartPoint, TransitionDuration);
                
            var col = newLevel.LevelCollider;
            var targetPos = col != null 
                ? col.bounds.center 
                : (newLevel.CameraPoint != null ? newLevel.CameraPoint.position : _sceneProvider.CameraTransform.position);
            if (_cameraController == null)
                return;
                
            var duration = TransitionDuration;
            var settings = _sceneProvider.GameSettings;
            var speed = Mathf.Max(0.0001f, settings.CameraMoveSpeed);
            if (settings != null && _sceneProvider.CameraTransform != null)
            {
                var camPos = _sceneProvider.CameraTransform.position;
                targetPos.z = camPos.z;
                var distance = Vector3.Distance(camPos, targetPos);
                duration = distance / speed;
            }
            _cameraController.SmoothMoveTo(targetPos, duration);
        }
		
		private void FitCameraToLevel(OneLevel level)
		{
			if (level == null || _sceneProvider == null)
				return;
			
			var camTransform = _sceneProvider.CameraTransform;
			if (camTransform == null)
				return;
			
			var cam = camTransform.GetComponent<Camera>();
			if (cam == null || !cam.orthographic)
				return;
			
			var col = level.LevelCollider;
			if (col == null)
				return;
			
			var bounds = col.bounds;
			var levelWidth = bounds.size.x;
			var levelHeight = bounds.size.y;
			var aspect = cam.aspect <= 0f ? 1f : cam.aspect;
			
			var sizeByHeight = levelHeight * 0.5f;
			var sizeByWidth = (levelWidth * 0.5f) / aspect;
			
			cam.orthographicSize = Mathf.Max(sizeByHeight, sizeByWidth);
		}
    }
}