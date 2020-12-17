using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class GameScreen : UiScreenBase
    {
        [SerializeField] private Text scoreText;

        private ScoreManager scoreManager;

        private void Awake()
        {
            scoreManager = GameManager.Instance.ScoreManager;
        }

        private void OnEnable()
        {
            UpdateScoreText(scoreManager.Score);
            scoreManager.ScoreChanged += ScoreManager_ScoreChanged;
        }

        private void OnDisable()
        {
            scoreManager.ScoreChanged -= ScoreManager_ScoreChanged;
        }

        private void UpdateScoreText(int score)
        {
            scoreText.text = score.ToString();
        }

        private void ScoreManager_ScoreChanged(int newScore)
        {
            UpdateScoreText(newScore);
        }
    }
}
