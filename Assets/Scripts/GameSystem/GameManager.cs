using SnakeGame.Behaviour;
using UnityEngine;

namespace SnakeGame.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region  Serialized Fields
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private GridMapManager _gridMapManager;
        [SerializeField] private SnakeController _snakeController;

        [Header("Game Settings")]
        [SerializeField] private float _moveDelay = 0.1f;
        [SerializeField] private Vector2 _snakeInitPosition = Vector2.zero;
        [SerializeField] private Vector2 _snakeInitDirection = Vector2.right;
        #endregion

        #region  Private Fields
        private Vector2 _gridMapSize = Vector2.zero;
        // Create InputManager if needed for handling user input
        #endregion

        #region Lifecycle Methods
        private void Start()
        {
            _gridMapSize = _gridMapManager.GenerateGridMap();
            CenterCamera();

            _snakeController.Spawn(_snakeInitPosition, _snakeInitDirection);
        }

        private void Update()
        {
            // Handle snake movement based on the move delay
            if (Time.time >= _moveDelay)
            {
                _snakeController.Move();
                _moveDelay = Time.time + _moveDelay;
            }
        }
        #endregion

        #region Private Methods
        private void HandleInput()
        {
            // Implement input handling logic to change snake direction
            // Example: Use arrow keys or WASD to set the snake's direction
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
