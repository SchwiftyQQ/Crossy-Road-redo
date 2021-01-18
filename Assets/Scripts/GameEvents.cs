using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameEvents : MonoBehaviour
    {
        #region quick Singleton
        public static GameEvents Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion

        public event Action<float> OnMovingObjectSpawn;
        public event Action<Vector3> OnTerrainGenerate;

        public event Action<Text> OnDailyCoinReward;
        public event Action<int> OnScoreIncrease;

        public event Action PlayerMovedUp;
        public event Action PlayerDied;


        public void SpawnAMovingOBject(float moveEndValue) => OnMovingObjectSpawn?.Invoke(moveEndValue);
        public void GenerateTerrain(Vector3 playerPos) => OnTerrainGenerate?.Invoke(playerPos);

        public void IncreaseScore(int score) => OnScoreIncrease?.Invoke(score);
        public void CollectReward(Text coinsText) => OnDailyCoinReward?.Invoke(coinsText);

        public void OnPlayerMovedUp() => PlayerMovedUp?.Invoke();
        public void OnPlayerDied() => PlayerDied?.Invoke();


    }
}