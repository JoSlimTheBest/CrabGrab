using UnityEngine;

namespace Game.Scripts.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private SceneProvider _sceneProvider;
        
        private GameManager _gameManager;
        private void Awake()
        {
            _gameManager = new GameManager(_sceneProvider); 
        }

        private void Update()
        {
            _gameManager.Update();
        }

		private void FixedUpdate()
		{
			_gameManager.FixedUpdate();
		}
    }
}
