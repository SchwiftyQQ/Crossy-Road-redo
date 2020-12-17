using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    public class UiManager : MonoBehaviour
    {
        #region quick Singleton
        public static UiManager Instance;

        private void Awake()
        {
            Instance = this;
        }
        #endregion


        [SerializeField] private UiScreenBinding[] screenPrefabBindings;
        [SerializeField] private Canvas canvas;

        private UiScreenBase activeScreen;

        public void ShowScreen(ScreenType screenType)
        {
            if (activeScreen != null)
            {
                Destroy(activeScreen.gameObject);
            }

            UiScreenBinding screenBinding = null;
            for (int i = 0; i < screenPrefabBindings.Length; i++)
            {
                if (screenPrefabBindings[i].ScreenType == screenType)
                {
                    screenBinding = screenPrefabBindings[i];
                    break;
                }
            }

            if (screenBinding == null)
            {
                throw new InvalidOperationException();
            }

            activeScreen = Instantiate(screenBinding.ScreenPrefab, canvas.transform);
        }
    }
}
