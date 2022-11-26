using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlaxEngine;

namespace Game
{
    public class LevelLoader : Script
    {
        private static LevelLoader _instance;

        public int transitionTime = 250;
        public Actor blackScreen = null;

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

        public static LevelLoader GetInstance()
        {
            return _instance;
        }


        public override void OnStart()
        {
            blackScreen.IsActive = false;
        }

        public async void PrepareForLoading(SceneReference sceneName)
        {
            blackScreen.IsActive = true;

            await Task.Delay(transitionTime);

            LoadScene(sceneName);
        }

        public void LoadScene(SceneReference scene)
        {
            Level.ChangeSceneAsync(scene);
        }
    }
}
