using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{    
    public class SubmenuManager : Script
    {
        private static SubmenuManager _instance;

        public override void OnAwake()
        {
            SetInstance();
        }

        private void SetInstance()
        {
            if (_instance != null)
            {
                Destroy(this.Actor);
                return;
            }

            _instance = this;
        }

        public static SubmenuManager GetInstance()
        {
            return _instance;
        }

        public event Action<int> onMainMenuButtonPressed;
        public event Action<string, float> onFeedback;

        public void MainMenuButtonPressed(int value)
        {
            if (onMainMenuButtonPressed != null)
            {
                onMainMenuButtonPressed(value);
            }
        }
        public void Feedback(string value, float time)
        {
            if (onFeedback != null)
            {
                onFeedback(value, time);
            }
        }
    }
}
