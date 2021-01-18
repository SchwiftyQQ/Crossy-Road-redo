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

            UiScreenBase screen = null;
            for (int i = 0; i < screenPrefabBindings.Length; i++)
            {
                if (screenPrefabBindings[i].ScreenType == screenType)
                {
                    screen = screenPrefabBindings[i].ScreenPrefab;
                    break;
                }
            }

            if (screen == null)
            {
                throw new InvalidOperationException();
            }

            if (canvas != null)
            {
                activeScreen = Instantiate(screen, canvas.transform);
            }
        }
    }
}
