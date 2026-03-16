using SnakeGame.Utility;
using UnityEngine;

namespace SnakeGame.Behaviour
{
    public class SnakeObjectPooler : ObjectPooler<SnakeObject>
    {
        #region Constructors
        public SnakeObjectPooler(SnakeObject prefab, Transform parentTransform, int initialSize = 0) : base(prefab, parentTransform, initialSize)
        {
        }
        #endregion
    }
}