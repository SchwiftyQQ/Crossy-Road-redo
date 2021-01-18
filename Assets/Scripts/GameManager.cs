using System.Collections;
using UnityEngine;
using Assets.Scripts.Ui;
using Assets.Scripts.SceneManagment;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        UiManager Ui;


        [SerializeField] private Player playerPrefab;

        public Player Player { get; private set; }

        public ScoreManager ScoreManager { get; private set; }

        public static bool gameIsPaused = false;

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

            ScoreManager = new ScoreManager();

            SceneDirector.Instance.LoadingFinished += ShowGameScreen;
            SceneDirector.Instance.LoadingFinished += SpawnPlayer;

            GameEvents.Instance.PlayerDied += ShowResultScreen;
        }

        private void SpawnPlayer()
        {
            Player = Instantiate(playerPrefab);
        }


        private void ShowGameScreen()
        {
            Ui.ShowScreen(ScreenType.Game);
        }

        private void ShowResultScreen()
        {
            Ui.ShowScreen(ScreenType.Result);
        }


        public static void PauseTheGame()
        {
            Time.timeScale = 0f;
            gameIsPaused = true;
        }

        public static void ResumeTheGame()
        {
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        private void OnDisable()
        {
            SceneDirector.Instance.LoadingFinished -= ShowGameScreen;
            SceneDirector.Instance.LoadingFinished -= SpawnPlayer;

            GameEvents.Instance.PlayerDied -= ShowResultScreen;
        }
    }
}