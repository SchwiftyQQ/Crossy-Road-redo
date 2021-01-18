using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.SceneManagment;

namespace Assets.Scripts.Ui
{
    //First thing that loads
    public class MainMenuScreen : UiScreenBase
    {
        [SerializeField] private Button startGameButton;

        private void OnEnable()
        {
            startGameButton.onClick.AddListener(StartGameButton_OnClick);
        }


        private void OnDisable()
        {
            startGameButton.onClick.RemoveListener(StartGameButton_OnClick);
        }


        //loads another scene where Loading Screen logic is
        private void StartGameButton_OnClick()
        {
            //UiManager.Instance.ShowScreen(ScreenType.Game);
            SceneManager.LoadSceneAsync((int)SceneIndexes.LOADING_SCREEN, LoadSceneMode.Additive);
            gameObject.SetActive(false);
        }
    }
}
