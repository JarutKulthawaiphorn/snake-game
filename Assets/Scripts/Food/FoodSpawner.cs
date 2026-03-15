using System.Collections.Generic;
using SnakeGame.Behaviour;
using UnityEngine;

namespace SnakeGame.Manager
{
    public class FoodSpawner : MonoBehaviour
    {
        #region  Serialized Fields
        [SerializeField] private FoodObject _foodPrefab;
        [SerializeField] private Transform _foodParent;

        [Header("Food Settings")]
        [SerializeField] private Vector2 _initFoodPosition = new Vector2(5, 5);
        #endregion

        #region  Private Fields
        private FoodObject _spawnedFood = null;
        #endregion

        #region  Public Methods
        public void SpawnFood()
        {
            if (_spawnedFood == null)
            {
                _spawnedFood = Instantiate(_foodPrefab, _foodParent);
            }

            _spawnedFood.SetPosition(_initFoodPosition);
        }

        public void SetFoodPosition(Vector2 position)
        {
            if (_spawnedFood != null)
            {
                _spawnedFood.SetPosition(position);
            }
        }

        public bool IsFoodAtPosition(Vector2 position)
        {
            if (_spawnedFood == null) return false;

            return _spawnedFood.Position == position;
        }
        #endregion
    }
}