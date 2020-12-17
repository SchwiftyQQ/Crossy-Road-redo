using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Ui
{
    public class ScoreManager
    {
        public event Action<int> ScoreChanged;
        public int Score { get; private set; }

        public ScoreManager(IPlayer player)
        {
            player.Died += CollectCoins;
        }

        void CollectCoins()
        {
            Score += UnityEngine.Random.Range(85, 111);
            ScoreChanged?.Invoke(Score);
        }
    }
}