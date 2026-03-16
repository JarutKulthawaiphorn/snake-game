using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame.UI
{
    public class GameStatUI : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private Text _scoreNumber = null;
        [SerializeField] private Text _highScoreNumber = null;
        #endregion

        #region Public Methods
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
        #endregion
    }
}