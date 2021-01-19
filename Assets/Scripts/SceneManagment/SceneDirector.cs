using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.Ui;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.SceneManagment
{
    public class SceneDirector : MonoBehaviour
    {
        public event Action LoadingFinished;

        [SerializeField] GameObject loadingScreenPrefab;
        [SerializeField] Canvas canvas;

        GameObject loadingScreen;

        public static SceneDirector Instance;
        private void Awake()
        {
            Instance = this;

            SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU_SCREEN, LoadSceneMode.Additive);
        }

        // initiates loading screen
        public void LoadGame()
        {
            loadingScreen = Instantiate(loadingScreenPrefab, canvas.transform);

            SceneManager.LoadSceneAsync((int)SceneIndexes.GAME_SCREEN, LoadSceneMode.Additive);

            StartCoroutine(FakeLoadingScreen());
        }

        IEnumerator FakeLoadingScreen()
        {
            GameManager.gameIsPaused = true;
            yield return new WaitForSeconds(loadingScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            GameManager.gameIsPaused = false;
            loadingScreen.SetActive(false);
            LoadingFinished.Invoke();
        }
       
    }
}