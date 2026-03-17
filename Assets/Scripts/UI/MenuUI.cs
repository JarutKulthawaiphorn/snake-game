using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace SnakeGame.UI
{
    public class MenuUI : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private Text _scoreNumber = null;
        [SerializeField] private Text _highScoreNumber = null;
        [SerializeField] private Button _startButton = null;
        #endregion

        #region Public Methods
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetScore(int score)
        {
            if (_scoreNumber != null)
            {
                _scoreNumber.text = score.ToString();
            }
        }

        public void SetHighScore(int highScore)
        {
            if (_highScoreNumber != null)
            {
                _highScoreNumber.text = highScore.ToString();
            }
        }

        public void SetStartButtonListener(UnityAction onClickAction)
        {
            _startButton.onClick.AddListener(onClickAction);
        }

        public void RemoveStartButtonListener(UnityAction onClickAction)
        {
            _startButton.onClick.RemoveListener(onClickAction);
        }
        #endregion
    }
}