using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class ScoreManager
    {
        public const string HIGH_SCORE_KEY = "Highscore";
        public const string COINS_KEY = "Coins";

        public int Score { get; private set; }
        public int HighScore { get; private set; }
        public int Coins { get; private set; }

        public ScoreManager()
        {
            GameEvents.Instance.PlayerMovedUp += IncreaseScore;
            GameEvents.Instance.OnDailyCoinReward += CollectCoins;
        }

        ~ScoreManager()
        {
            GameEvents.Instance.PlayerMovedUp -= IncreaseScore;
            GameEvents.Instance.OnDailyCoinReward -= CollectCoins;
        }

        void IncreaseScore()
        {
            Score += 1;
            GameEvents.Instance.IncreaseScore(Score);

            HighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);

            int hScore = 0;
            hScore += Score;
            if (hScore >= PlayerPrefs.GetInt(HIGH_SCORE_KEY))
            {
                HighScore = hScore;
            }

            PlayerPrefs.SetInt(HIGH_SCORE_KEY, HighScore);
        }

        void CollectCoins(Text coinsText)
        {
            int rewardCoins = UnityEngine.Random.Range(0, 101);
            Coins = +rewardCoins;
            PlayerPrefs.SetInt(COINS_KEY, Coins);
            coinsText.text = PlayerPrefs.GetInt(COINS_KEY).ToString();
        }
    }
}