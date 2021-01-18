using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class OptionsScreen : UiScreenBase
    {
        [SerializeField] Button resetHighScoreButton;
        [SerializeField] Button backToGameScreenButton;

        private void OnEnable()
        {

            resetHighScoreButton.onClick.AddListener(ResetHighScore);
            backToGameScreenButton.onClick.AddListener(BackToGameScreen);
        }
        private void OnDisable()
        {
            resetHighScoreButton.onClick.RemoveListener(ResetHighScore);
            backToGameScreenButton.onClick.RemoveListener(BackToGameScreen);
        }

        void ResetHighScore()
        {
            PlayerPrefs.DeleteKey(ScoreManager.HIGH_SCORE_KEY);
        }

        void BackToGameScreen()
        {
            UiManager.Instance.ShowScreen(ScreenType.Game);
            GameManager.ResumeTheGame();
        }

    }
}