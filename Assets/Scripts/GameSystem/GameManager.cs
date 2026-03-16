using UnityEngine;

namespace SnakeGame.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region  Serialized Fields
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private GameplayRunner _gameplayRunner;
        [SerializeField] private UIManager _uiManager;
        #endregion

        #region  Private Fields
        private Vector2 _gridMapSize = Vector2.zero;
        private int _score = 0;
        private int _highScore = 0;
        #endregion

        #region Lifecycle Methods
        private void Start()
        {
            Initialize();
            SetCallback();
            _uiManager.ShowMenuUI(_score, _highScore);
            OnGameStart();
        }
        #endregion

        #region Private Methods
        private void Initialize()
        {
            _gameplayRunner.Initialize();
            _gridMapSize = _gameplayRunner.GridMapSize;

            CenterCamera();
        }

        private void OnGameStart()
        {
            _gameplayRunner.StartGame();
            _uiManager.SetGameStatUIScore(0);
            _uiManager.SetGameStatUIHighScore(_highScore);
        }

        private void OnGameOver()
        {
            _uiManager.ShowMenuUI(_score, _highScore);
            _gameplayRunner.SetupGameplay();
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
        
        #region Callback Methods
        private void OnStartButtonClicked()
        {
            _score = 0;
            OnGameStart();
            _uiManager.HideMenuUI();
        }

        private void OnConsumeFood()
        {
            _score++;
            _uiManager.SetGameStatUIScore(_score);

            if (_score > _highScore)
            {
                _highScore = _score;
                _uiManager.SetGameStatUIHighScore(_highScore);
            }
        }
        #endregion

        private void SetCallback()
        {
            _gameplayRunner.SetOnConsumeFoodCallback(OnConsumeFood);
            _gameplayRunner.SetOnGameOverCallback(OnGameOver);
            _uiManager.SetMenuUIStartButtonListener(OnStartButtonClicked);
        }
        #endregion
    }
}
