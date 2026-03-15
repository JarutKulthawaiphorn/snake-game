using SnakeGame.Behaviour;
using UnityEngine;

namespace SnakeGame.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region  Serialized Fields
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private GameplayRunner _gameplayRunner;
        #endregion

        #region  Private Fields
        private Vector2 _gridMapSize = Vector2.zero;
        #endregion

        #region Lifecycle Methods
        private void Start()
        {
            Initialize();
            OnGameStart();
        }
        #endregion

        #region Private Methods
        private void Initialize()
        {
            _gameplayRunner.Initialize();
            _gridMapSize = _gameplayRunner.GridMapSize;
            
            CenterCamera();
        }

        private void SetupGameplay()
        {
            _gameplayRunner.SetupGameplay();
        }

        private void OnGameStart()
        {
            _gameplayRunner.StartGame();
        }

        private void OnGameOver()
        {
            _gameplayRunner.GameOver();
        }

        private void CenterCamera()
        {
            if (_mainCamera != null)
            {
                float offsetX = (_gridMapSize.x - 1) / 2f;
                float offsetY = (_gridMapSize.y - 1) / 2f;
                _mainCamera.position = new Vector3(offsetX, offsetY, _mainCamera.position.z);
            }
        }
        #endregion
    }
}
