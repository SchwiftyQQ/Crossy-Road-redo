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
        private GameEvents events;

        private void Awake()
        {
            scoreManager = GameManager.Instance.ScoreManager;
            events = GameEvents.Instance;
        }

        private void OnEnable()
        {
            UpdateScoreText(scoreManager.Score);
            events.ScoreChanged += ScoreManager_ScoreChanged;
        }

        private void OnDisable()
        {
            events.ScoreChanged -= ScoreManager_ScoreChanged;
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
