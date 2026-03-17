using System;
using System.Collections.Generic;
using SnakeGame.Behaviour;
using UnityEngine;

namespace SnakeGame.Manager
{
    public class GameplayRunner : MonoBehaviour
    {
        #region  Serialized Fields
        [SerializeField] private GridMapManager _gridMapManager;
        [SerializeField] private SnakeController _snakeController;
        [SerializeField] private FoodSpawner _foodSpawner;

        [Header("Game Settings")]
        [SerializeField] private Vector2 _snakeInitPosition = Vector2.zero;
        [SerializeField] private Vector2 _snakeInitDirection = Vector2.right;
        [SerializeField] private float _moveDelay = 0.1f;
        #endregion

        #region  Public Properties
        public Vector2 GridMapSize => _gridMapSize;
        #endregion

        #region Action Fields
        private Action _onConsumeFood = null;
        private Action _onGameOver = null;
        #endregion

        #region  Private Fields
        private Vector2 _gridMapSize = Vector2.zero;
        private float _moveTimer = 0f;
        private bool _isGameStarted = false;
        private SnakeInputManager _snakeInputManager = null;
        #endregion

        #region  Lifecycle Methods
        private void Update()
        {
            if (_isGameStarted == false) return;

            _moveTimer -= Time.deltaTime;

            if (_moveTimer <= 0)
            {
                MoveSnake();
            }
        }
        #endregion

        #region Public Methods
        public void Initialize()
        {
            _isGameStarted = false;

            _snakeController.Initialize();
            _snakeInputManager = new SnakeInputManager();

            SetupGridMap();
            SetupGameplay();
        }

        public void SetupGameplay()
        {
            SetupSnake();
            SetupFood();
        }

        public void StartGame()
        {
            _snakeInputManager.EnableSnakeInput(HandleInput);
            _isGameStarted = true;

            _moveTimer = _moveDelay;
        }

        public void GameOver()
        {
            _snakeInputManager.DisableSnakeInput();
            _isGameStarted = false;

            Debug.Log("Game Over!");
            _onGameOver?.Invoke();
        }

        public void SetOnConsumeFoodCallback(Action onConsumeFood)
        {
            _onConsumeFood = onConsumeFood;
        }

        public void SetOnGameOverCallback(Action onGameOver)
        {
            _onGameOver = onGameOver;
        }
        #endregion

        #region  Private Methods
        private void SetupGridMap()
        {
            _gridMapSize = _gridMapManager.GenerateGridMap();
        }

        private void SetupSnake()
        {
            _snakeController.Spawn(_snakeInitPosition, _snakeInitDirection);
        }

        private void SetupFood()
        {
            _foodSpawner.SpawnFood();
        }

        private void HandleInput(Vector2 direction)
        {
            if (_snakeController.IsValidDirection(direction) == false || _isGameStarted == false) return;

            _snakeController.SetDirection(direction);
        }

        private void MoveSnake()
        {
            Vector2 nextPosition = _snakeController.GetNextPosition();

            if (_gridMapManager.IsPositionInsideGrid(nextPosition) == false || _snakeController.IsPositionOnSnake(nextPosition))
            {
                GameOver();
                return;
            }

            if (_foodSpawner.IsFoodAtPosition(nextPosition))
            {
                OnSnakeConsumeFood();
            }

            _snakeController.Move();
            _moveTimer = _moveDelay;
        }

        private void OnSnakeConsumeFood()
        {
            _snakeController.Grow();
            _foodSpawner.SetFoodPosition(GetRandomFoodPosition());
            _onConsumeFood?.Invoke();
        }

        private Vector2 GetRandomFoodPosition()
        {
            List<Vector2> availablePositions = GetAvailablePositionList();
            if (availablePositions.Count == 0) return Vector2.zero;

            int randomIndex = UnityEngine.Random.Range(0, availablePositions.Count);
            return availablePositions[randomIndex];
        }

        private List<Vector2> GetAvailablePositionList()
        {
            List<Vector2> availablePositions = new List<Vector2>();

            foreach (var position in _gridMapManager.TilePositionList)
            {
                if (_snakeController.IsPositionOnSnake(position) == false && _foodSpawner.IsFoodAtPosition(position) == false)
                {
                    availablePositions.Add(position);
                }
            }

            if (availablePositions.Count == 0)
            {
                GameOver();
            }

            return availablePositions;
        }
        #endregion
    }
}