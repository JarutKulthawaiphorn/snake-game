using SnakeGame.UI;
using UnityEngine;
using UnityEngine.Events;

namespace SnakeGame.Manager
{
    public class UIManager : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private MenuUI _menuUI = null;
        [SerializeField] private GameStatUI _gameStatUI = null;
        #endregion

        #region Public Methods
        public void ShowMenuUI(int score, int highScore)
        {
            _menuUI.SetActive(true);
            _menuUI.SetScore(score);
            _menuUI.SetHighScore(highScore);
        }

        public void HideMenuUI()
        {
            _menuUI.SetActive(false);
        }

        public void SetMenuUIStartButtonListener(UnityAction onClickAction)
        {
            _menuUI.SetStartButtonListener(onClickAction);
        }

        public void RemoveMenuUIStartButtonListener(UnityAction onClickAction)
        {
            _menuUI.RemoveStartButtonListener(onClickAction);
        }

        public void SetGameStatUIScore(int score)
        {
            _gameStatUI.SetScore(score);
        }

        public void SetGameStatUIHighScore(int highScore)
        {
            _gameStatUI.SetHighScore(highScore);
        }
        #endregion
    }
}