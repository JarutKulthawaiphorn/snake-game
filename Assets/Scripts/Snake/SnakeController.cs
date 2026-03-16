using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Behaviour
{
    public class SnakeController : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private SnakeObject _snakePrefab = null;
        [SerializeField] private int _initSize = 3;
        #endregion

        #region  Private Fields
        private SnakeObjectPooler _snakeObjectPooler = null;
        private List<SnakeObject> _snakeSegmentList = new List<SnakeObject>();
        private Vector2 _currentDirection = Vector2.right;
        private Vector2 _nextDirection = Vector2.right;
        #endregion

        #region  Public Methods
        public void Initialize()
        {
            _snakeObjectPooler = new SnakeObjectPooler(_snakePrefab, transform, _initSize);
        }

        public void Spawn(Vector2 initPosition, Vector2 initDirection)
        {
            ResetSnake();
            _currentDirection = initDirection;
            _nextDirection = initDirection;

            SnakeObject headSegment = _snakeObjectPooler.GetObject();
            headSegment.transform.position = initPosition;
            headSegment.name = "SnakeHead";
            _snakeSegmentList.Add(headSegment);

            for (int i = 1; i < _initSize; i++)
            {
                Grow();
            }
        }

        public void ResetSnake()
        {
            _snakeObjectPooler.ReturnObjectList(_snakeSegmentList);
            _snakeSegmentList.Clear();
        }

        public void SetDirection(Vector2 direction)
        {
            if (IsValidDirection(direction))
            {
                _nextDirection = direction;
            }
        }

        public Vector2 GetNextPosition()
        {
            return _snakeSegmentList[0].transform.localPosition + (Vector3)_nextDirection;
        }

        public void Move()
        {
            _currentDirection = _nextDirection;
            for (int i = _snakeSegmentList.Count - 1; i > 0; i--)
            {
                _snakeSegmentList[i].SetPosition(_snakeSegmentList[i - 1].Position);
            }
            _snakeSegmentList[0].SetPosition(GetNextPosition());
        }

        public void Grow()
        {
            SnakeObject lastSegment = _snakeSegmentList[_snakeSegmentList.Count - 1];
            Vector2 initPosition = lastSegment.transform.position - (Vector3)_currentDirection;

            SnakeObject newSegment = Instantiate(_snakePrefab, initPosition, Quaternion.identity, transform);
            newSegment.name = "SnakeSegment " + _snakeSegmentList.Count;
            _snakeSegmentList.Add(newSegment);
        }

        public bool IsValidDirection(Vector2 direction)
        {
            return direction != -_currentDirection && direction != _currentDirection;
        }

        public bool IsPositionOnSnake(Vector2 position)
        {
            foreach (var segment in _snakeSegmentList)
            {
                if (segment.Position == position)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}