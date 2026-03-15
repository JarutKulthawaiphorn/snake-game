using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Behaviour
{
    public class SnakeController : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private SnakeEntity _snakePrefab = null;
        [SerializeField] private int _initSize = 3;
        #endregion

        #region  Private Fields
        private List<SnakeEntity> _snakeSegmentList = new List<SnakeEntity>();
        private Vector2 _currentDirection = Vector2.right;
        #endregion

        #region  Public Methods
        public void Spawn(Vector2 initPosition, Vector2 initDirection)
        {
            SetDirection(initDirection);

            SnakeEntity headSegment = Instantiate(_snakePrefab, initPosition, Quaternion.identity, transform);
            headSegment.name = "SnakeHead";
            _snakeSegmentList.Add(headSegment);

            for (int i = 1; i < _initSize; i++)
            {
                Grow();
            }
        }

        public void SetDirection(Vector2 direction)
        {
            bool isValidDirection = direction != -_currentDirection;

            if (isValidDirection)
            {
                _currentDirection = direction;
            }
        }

        public Vector2 GetNextPosition()
        {
            return _snakeSegmentList[0].transform.localPosition + (Vector3)_currentDirection;
        }

        public void Move()
        {
            for (int i = _snakeSegmentList.Count - 1; i > 0; i--)
            {
                _snakeSegmentList[i].SetPosition(_snakeSegmentList[i - 1].Position);
            }
            _snakeSegmentList[0].SetPosition(GetNextPosition());
        }

        public void Grow()
        {
            SnakeEntity lastSegment = _snakeSegmentList[_snakeSegmentList.Count - 1];
            Vector2 initPosition = lastSegment.transform.position - (Vector3)_currentDirection;

            SnakeEntity newSegment = Instantiate(_snakePrefab, initPosition, Quaternion.identity, transform);
            newSegment.name = "SnakeSegment " + _snakeSegmentList.Count;
            _snakeSegmentList.Add(newSegment);
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