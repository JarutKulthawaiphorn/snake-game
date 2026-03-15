using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Manager
{
    public class GridMapManager : MonoBehaviour
    {
        #region  Serialized Fields
        [SerializeField] private Transform _gridMapContainer;

        [Header("Tile Settings")]
        [SerializeField] private SpriteRenderer _tilePrefab;
        [SerializeField] private Color _whiteTileColor = Color.white;
        [SerializeField] private Color _blackTileColor = Color.black;

        [Header("Grid Map Settings")]
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        #endregion

        #region Public Properties
        public List<Vector2> TilePositionList => _tilePositionList;
        #endregion

        #region Private Fields
        private List<Vector2> _tilePositionList = new List<Vector2>();
        #endregion

        #region  Public Methods
        public Vector2 GenerateGridMap()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    SpriteRenderer tile = Instantiate(_tilePrefab, new Vector3(x, y, 0), Quaternion.identity, _gridMapContainer);
                    tile.color = (x + y) % 2 == 0 ? _whiteTileColor : _blackTileColor;
                    tile.name = $"GridTile {x}, {y}";
                    _tilePositionList.Add(new Vector2(x, y));
                }
            }

            return new Vector2(_width, _height);
        }

        public bool IsPositionInsideGrid(Vector2 position)
        {
            return position.x >= 0 && position.x < _width && position.y >= 0 && position.y < _height;
        }
        #endregion
    }
}