using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


        public event Action<float> OnMovingOBjectSpawn;
        public event Action<Collider> onTreeTriggerEnter;
        public event Action<Vector3> onMoreTerrainSpawn;
        public event Action<int> ScoreChanged;
        public event Action PlayerDied;


        public void SpawnAMovingOBject(float moveEndValue) => OnMovingOBjectSpawn?.Invoke(moveEndValue);
        public void OnTreeTriggerEnter(Collider other) => onTreeTriggerEnter?.Invoke(other);
        public void OnMoreTerrainSpawn(Vector3 playerPos) => onMoreTerrainSpawn?.Invoke(playerPos);
        public void OnScoreChanged(int score) => ScoreChanged?.Invoke(score);
        public void OnPlayerDied() => PlayerDied?.Invoke();
    }
}