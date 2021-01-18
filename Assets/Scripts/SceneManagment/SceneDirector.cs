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

        [SerializeField] GameObject loadingScreen;
        [SerializeField] Canvas canvas;
        [SerializeField] Animator animator;
        

        public static SceneDirector Instance;
        private void Awake()
        {
            Instance = this;

        }

        private void Start()
        {
            LoadGame();
        }

        public void LoadGame()
        {
            loadingScreen.SetActive(true);

            SceneManager.LoadSceneAsync((int)SceneIndexes.GAME_SCREEN, LoadSceneMode.Additive);

            StartCoroutine(FakeLoadingScreen());
        }

        IEnumerator FakeLoadingScreen()
        {
            GameManager.gameIsPaused = true;
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            GameManager.gameIsPaused = false;
            loadingScreen.SetActive(false);
            LoadingFinished.Invoke();
        }
       
    }
}