using System.Collections;
using UnityEngine;
using Assets.Scripts.Ui;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        UiManager Ui;


        [SerializeField] private Player playerPrefab;

        public Player Player { get; private set; }

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
            Ui = UiManager.Instance;

            Player = Instantiate(playerPrefab);
            ScoreManager = new ScoreManager();
            Ui.ShowScreen(ScreenType.MainMenu);
        }
    }
}