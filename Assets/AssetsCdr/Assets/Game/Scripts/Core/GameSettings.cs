using UnityEngine;

namespace Game.Scripts.Core
{
	[CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Game Settings")]
	public class GameSettings : ScriptableObject
	{
		[SerializeField]
		private float _playerMoveSpeed = 5f;

		[SerializeField]
		private float _cameraMoveSpeed = 10f;

		public float PlayerMoveSpeed => _playerMoveSpeed;
		public float CameraMoveSpeed => _cameraMoveSpeed;
	}
}

