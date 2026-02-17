using UnityEngine;

namespace Game.Scripts.Core.CameraFeature
{
	public class CameraController
	{
		private readonly Transform _cameraTransform;
		
		private bool _isMoving;
		private Vector3 _startPosition;
		private Vector3 _targetPosition;
		private float _duration;
		private float _elapsed;

		public CameraController(Transform cameraTransform)
		{
			_cameraTransform = cameraTransform;
		}

		public void SmoothMoveTo(Vector3 targetPosition, float duration)
		{
			if (_cameraTransform == null) 
				return;
			
			_startPosition = _cameraTransform.position;
			targetPosition.z = _startPosition.z;
			_targetPosition = targetPosition;
			_duration = Mathf.Max(0.0001f, duration);
			_elapsed = 0f;
			_isMoving = true;
		}
		
		public void SmoothMoveTo(Transform target, float duration)
		{
			if (target == null) 
				return;
			
			SmoothMoveTo(target.position, duration);
		}
		
		public void Update(float deltaTime)
		{
			if (!_isMoving || _cameraTransform == null) 
				return;
			
			_elapsed += deltaTime;
			var t = Mathf.Clamp01(_elapsed / _duration);
			var pos = Vector3.Lerp(_startPosition, _targetPosition, t);
			pos.z = _startPosition.z;
			_cameraTransform.position = pos;
			if (t >= 1f) 
				_isMoving = false;
		}
	}
}

