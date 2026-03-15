using UnityEngine;

namespace SnakeGame.Behaviour
{
    public class SnakeEntity : MonoBehaviour
    {
        #region  Serialized Fields
        [SerializeField] private SpriteRenderer _spriteRenderer;
        #endregion

        #region Properties
        public Vector2 Position => transform.position;
        #endregion

        #region  Public Methods
        public void SetPosition(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, 0);
        }
        #endregion
    }
}