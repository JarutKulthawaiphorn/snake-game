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
        [SerializeField] private FoodSpawner _foodSpawner;

        [Header("Game Settings")]
        [SerializeField] private float _moveDelay = 0.1f;
        [SerializeField] private Vector2 _snakeInitPosition = Vector2.zero;
        [SerializeField] private Vector2 _snakeInitDirection = Vector2.right;
        #endregion

        #region  Private Fields
        private float _moveTimer = 0f;
        private Vector2 _gridMapSize = Vector2.zero;
        private SnakeInputManager _snakeInputManager = null;
        private bool _isGameStarted = false;
        #endregion

        #region Lifecycle Methods
        private void Start()
        {
            Initialize();
            SetupGridMap();
            SetupSnake();
            SetupFood();

            OnGameStart();
        }

        private void Update()
        {
            if (_isGameStarted == false) return;

            _moveTimer -= Time.deltaTime;

            if (_moveTimer <= 0)
            {
                Vector2 nextPosition = _snakeController.GetNextPosition();
                if (_gridMapManager.IsPositionInsideGrid(nextPosition) == false || _snakeController.IsPositionOnSnake(nextPosition))
                {
                    OnGameOver();
                    return;
                }
                MoveSnake();
            }
        }
        #endregion

        #region Private Methods
        private void Initialize()
        {
            _snakeInputManager = new SnakeInputManager();
        }

        private void SetupGridMap()
        {
            _gridMapSize = _gridMapManager.GenerateGridMap();
            CenterCamera();
        }

        private void SetupSnake()
        {
            _snakeController.Spawn(_snakeInitPosition, _snakeInitDirection);
        }

        private void SetupFood()
        {
            _foodSpawner.SpawnFood();
        }

        private void OnGameStart()
        {
            _snakeInputManager.EnableSnakeInput(HandleInput);
            _isGameStarted = true;

            _moveTimer = _moveDelay;
        }

        private void OnGameOver()
        {
            _snakeInputManager.DisableSnakeInput();
            _isGameStarted = false;

            Debug.Log("Game Over!");
        }

        private void HandleInput(Vector2 direction)
        {
            _snakeController.SetDirection(direction);
            MoveSnake();
        }

        private void MoveSnake()
        {
            _snakeController.Move();
            _moveTimer = _moveDelay;
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
