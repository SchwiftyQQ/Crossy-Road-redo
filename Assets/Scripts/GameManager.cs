using System.Collections;
using UnityEngine;
using Assets.Scripts.Ui;
using Assets.Scripts.SceneManagment;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        // RV: Inconsistent visibility modifier (implicit 'private')
        UiManager Ui;


        [SerializeField] private Player playerPrefab;

        public Player Player { get; private set; }

        public ScoreManager ScoreManager { get; private set; }

        // RV: You're writing to this variable directly
        // and using {Pause/Resume}TheGame. It's difficult to understand
        // when and where you can choose a correct API.
        // So it would be nice if this variable was private and
        // the only way to control game pause were {Pause/Resume}TheGame methods.
        // And again: public fields - bad style
        //
        // When you remove GameManager access at SceneDirector, this field shouldn't be static.
        public static bool gameIsPaused = false;

        #region quick Singleton

        // RV: Should be get; private set; property
        public static GameManager Instance;
        private void Awake()
        {
            // RV: If you're using some static thing multiple times
            // (Instance gets written with a new value every level start)
            // you should reset this to null at some counterpart point (e.g. at OnDestroy).
            // This will ensure that you're not referencing dead objects.
            // Common counterparts are:
            // Awake/OnDestroy
            // Start/OnDestroy
            // OnEnable/OnDisable.
            // The upper-level idea of that: when you acquire some resource
            // then you should release it too. In this case Instance is a kind of that resource.
            Instance = this;
        }
        #endregion

        private void Start()
        {
            Ui = UiManager.Instance;

            ScoreManager = new ScoreManager();

            // RV: Event subscriptions and unsubscriptions should be performed
            // at counterpart methods
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

        // RV: Potential bug: when a level gets destroyed on pause,
        // timeScale will remain 0f. Some a game will stuck.
        // You should release timeScale at some kind of OnDestroy point if a game
        // was paused indeed
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