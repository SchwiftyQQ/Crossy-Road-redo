using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
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


        private void StartGameButton_OnClick()
        {
            UiManager.Instance.ShowScreen(ScreenType.Game);
        }
    }
}
