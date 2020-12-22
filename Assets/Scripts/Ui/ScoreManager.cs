using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Ui
{
    public class ScoreManager
    {

        public int Score { get; private set; }

        public ScoreManager()
        {
            GameEvents.Instance.PlayerDied += CollectCoins;
        }

        void CollectCoins()
        {
            Score += UnityEngine.Random.Range(85, 111);
            GameEvents.Instance.OnScoreChanged(Score);

            UiManager.Instance.ShowScreen(ScreenType.Result);
        }
    }
}