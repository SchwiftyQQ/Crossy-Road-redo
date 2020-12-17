using System.Collections;
using UnityEngine;
using Assets.Scripts.Ui;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player playerPrefab;

        public IPlayer Player { get; private set; }

        public ScoreManager ScoreManager { get; private set; }

        #region quick Singleton
        public static GameManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion


        private void Start()
        {
            Player = Instantiate(playerPrefab);
            ScoreManager = new ScoreManager(Player);
            UiManager.Instance.ShowScreen(ScreenType.MainMenu);
        }
    }
}