using System;
using UnityEngine;

namespace Game.Scripts.Core.Level.View
{
	public class OneLevel : MonoBehaviour
	{
		[SerializeField]
		private Transform _startPoint;

		[SerializeField]
		private Transform _cameraPoint;

		[SerializeField]
		private GameObject _visualRoot;

		[SerializeField]
		private Collider2D _levelCollider;

		[SerializeField] private int _levelID; // уникальный ID уровня
		public int LevelID => _levelID;

		public SignWay signWay; // ссылка на компонент SignWay для анимации указателя



		public Transform StartPoint => _startPoint;
		public Transform CameraPoint => _cameraPoint;
		public GameObject VisualRoot => _visualRoot;
		public Collider2D LevelCollider => _levelCollider;

		public event Action onEnter;

		private void Awake()
		{
			if (_visualRoot != null)
				_visualRoot.SetActive(false);
		}

		private void OnValidate()
		{
			if (_levelCollider == null)
				_levelCollider = GetComponent<Collider2D>();

			if (_visualRoot == null)
			{
				var visual = transform.Find("Visual");
				if (visual != null)
					_visualRoot = visual.gameObject;
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other == null)
				return;

			if (!other.CompareTag("Player"))
				return;

			onEnter?.Invoke();
		}

		public void SetVisualEnabled(bool enabled)
		{
			if (_visualRoot != null)
				_visualRoot.SetActive(enabled);

			if (signWay != null)
				signWay.AnimationLevel(_levelID, _startPoint);
		}

        public void RestartLevel(Transform player)
        {
            if (_startPoint != null)
                player.position = _startPoint.position;

            SetVisualEnabled(false);
            SetVisualEnabled(true);
        }


    }
}