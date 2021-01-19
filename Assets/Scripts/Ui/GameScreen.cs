using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class GameScreen : UiScreenBase
    {
        [SerializeField] Button optionsScreenButton;

        [SerializeField] private Text scoreText;

        private ScoreManager scoreManager;
        private GameEvents events;

        private void Awake()
        {
            scoreManager = GameManager.Instance.ScoreManager;
            events = GameEvents.Instance;

            optionsScreenButton.onClick.AddListener(OpenOptionsScreen);
        }

        private void OnEnable()
        {
            UpdateScoreText(scoreManager.Score);

            events.OnScoreIncrease += ScoreManager_IncreaseScore;
        }

        private void OnDisable()
        {
            events.OnScoreIncrease -= ScoreManager_IncreaseScore;
        }

        private void UpdateScoreText(int score)
        {
            scoreText.text = score.ToString();
        }

        private void ScoreManager_IncreaseScore(int newScore)
        {
            UpdateScoreText(newScore);
        }


        private void OpenOptionsScreen()
        {
            UiManager.Instance.ShowScreen(ScreenType.Options);
            GameManager.PauseTheGame();
        }

    }
}
