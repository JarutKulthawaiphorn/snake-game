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

        public void Move()
        {
            Vector3 nextPosition = _snakeSegmentList[0].transform.localPosition + (Vector3)_currentDirection;
            for (int i = _snakeSegmentList.Count - 1; i > 0; i--)
            {
                _snakeSegmentList[i].SetPosition(_snakeSegmentList[i - 1].Position);
            }
            _snakeSegmentList[0].SetPosition(nextPosition);
        }

        public void Grow()
        {
            SnakeEntity lastSegment = _snakeSegmentList[_snakeSegmentList.Count - 1];
            Vector2 initPosition = lastSegment.transform.position - (Vector3)_currentDirection;

            SnakeEntity newSegment = Instantiate(_snakePrefab, initPosition, Quaternion.identity, transform);
            newSegment.name = "SnakeSegment " + _snakeSegmentList.Count;
            _snakeSegmentList.Add(newSegment);
        }

        public List<Vector2> GetSnakePositionList()
        {
            List<Vector2> positionList = new List<Vector2>();

            foreach (var segment in _snakeSegmentList)
            {
                positionList.Add(segment.Position);
            }

            return positionList;
        }
        #endregion
    }
}