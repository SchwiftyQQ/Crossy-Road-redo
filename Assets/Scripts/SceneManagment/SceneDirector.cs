// RV: You should google how to cleanup and refactor code in your IDE.
// One of things that will be performed is imports cleanup.
// This action should become a habit: you have written some lines, hit cleanup hotkey.
// Then your code style will become cleaner.
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.Ui;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.SceneManagment
{
    // RV: SceneDirector isn't a best name for this class.
    // As I see its responsibility is to load game scene behind some loadingScreen.
    // So a name like 'GameSceneLoader' would be more appropriate.
    public class SceneDirector : MonoBehaviour
    {
        public event Action LoadingFinished;

        // RV: You should stick to some single visibility modifiers policy.
        // These fields are actually private.
        // Below you are marking Awake method as private explicitly.
        // So you should either mark your fields as private too or remove 'private' modifiers from
        // private members of your class. I prefer the first way. It increases readability.
        [SerializeField] GameObject loadingScreenPrefab;
        [SerializeField] GameObject loadingScreenPrefabWebGL;

        [SerializeField] Canvas canvas;

        GameObject loadingScreen;

        // RV: You shouldn't expose your fields with a public modifier.
        // You can write and read public field reference wherever in your project.
        // This is kind of uncontrollable API.
        // The better way of making some reference be publicly readable
        // is to wrap this field in a property with readonly access:
        // public static SceneDirector Instance { get; private set; }
        // Such type of access will prevent corruption of Instance member
        // somewhere in other place than SceneDirector class.
        public static SceneDirector Instance;
        private void Awake()
        {
            Instance = this;

            // RV: If you want to make some monobehaviour as a static thing in all things of your game
            // you can call DontDestroyOnLoad on your monobehaviour instance. Then you are not forced
            // to use LoadSceneMode.Additive to leave SceneDirector instance alive when you
            // change scenes
            SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU_SCREEN, LoadSceneMode.Additive);
        }

        // initiates loading screen
        public void LoadGame()
        {
            // RV: You should always be as closer to a device build state as possible.
            // Hence you should avoid things as #if UNITY_SOMEPLATFORM when it's reasonable.
#if UNITY_WEBGL
            loadingScreen = Instantiate(loadingScreenPrefabWebGL, canvas.transform);
#elif UNITY_ANDROID
            loadingScreen = Instantiate(loadingScreenPrefab, canvas.transform);
#endif

            // RV: LoadSceneMode.Additive should be used when you have some scene as overlay.
            // I mean overlay as some visual content (e.g. HUD) or
            // objects that should live across scenes (e.g. character moving to next level)

            // RV: LoadSceneAsync returns AsyncOperation. That means that you can await
            // a result of scene loading in a coroutine.
            // If you want to hide scene loading behind some UI panel you can do it like in
            // ExLoadGameScreenScene method below
            SceneManager.LoadSceneAsync((int)SceneIndexes.GAME_SCREEN, LoadSceneMode.Additive);

            StartCoroutine(FakeLoadingScreen());
        }

        IEnumerator FakeLoadingScreen()
        {
            GameManager.gameIsPaused = true;
            // RV: Here I get NRE (NullReferenceException) on loadingScreen variable in Editor.
            // yield return new WaitForSeconds(loadingScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            yield return new WaitForSeconds(loadingScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            GameManager.gameIsPaused = false;
            loadingScreen.SetActive(false);

            // RV: When you fire event you should use ?. operator. (LoadingFinished?.Invoke();)
            // If no one is subscribed on that event by the moment of fire you will get an exception
            LoadingFinished.Invoke();
        }


        // RV:
        // IEnumerator ExLoadGameScreenScene()
        // {
        //     // Enable an overlay to hide loading.
        //     loadingScreen.SetActive(true);
        //     AsyncOperation loadSceneOp = SceneManager.LoadSceneAsync(someSceneIndex, someLoadMode);
        //     while (!loadSceneOp.isDone)
        //     {
        //         yield return null;
        //     }
        //
        //     // New scene is loaded. Now you can disable an overlay.
        //     loadingScreen.SetActive(false);
        // }
    }
}