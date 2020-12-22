using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui
{
    public class ResultScreen : UiScreenBase
    {
        [SerializeField] private Button playAgainButton;



        private void OnEnable()
        {
            playAgainButton.onClick.AddListener(OnPlayAgainButtonClick);
        }

        private void OnDisable()
        {
            playAgainButton.onClick.RemoveListener(OnPlayAgainButtonClick);
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnPlayAgainButtonClick()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }
}