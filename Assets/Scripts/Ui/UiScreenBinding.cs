using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    [Serializable]
    public class UiScreenBinding
    {
        [SerializeField] private ScreenType screenType;
        [SerializeField] private UiScreenBase screenPrefab;

        public ScreenType ScreenType
        {
            get
            {
                return screenType;
            }
        }


        public UiScreenBase ScreenPrefab
        {
            get
            {
                return screenPrefab;
            }
        }
    }
}
