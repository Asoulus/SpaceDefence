using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game
{
    public class MenuButton : Script
    {
        public Actor selector;

        public SceneReference demoScene;
        public SceneReference level_Scene;

        public AudioSource pressSound;

        protected bool isSelected = false;
        public string buttonType;

        [Tooltip("The menu buttons.")]
        public List<UIControl> Buttons;

        private LevelLoader levelLoader;

        public override void OnStart()
        {
            if (Buttons == null || Buttons.Count == 0)
            {
                Debug.Log("No buttons");
                return;
            }

            foreach (var button in Buttons)
            {
                button.Get<Button>().Clicked += ButtonPressed;
            }

            levelLoader = LevelLoader.GetInstance();
        }

        protected void ButtonPressed()
        {
            Pressed();

            switch (buttonType)
            {
                case "Exit":
                    {
                        Engine.RequestExit();
                    }
                    break;
                case "Play":
                    {
                        levelLoader.PrepareForLoading(level_Scene);
                    }
                    break;
                case "Demo":
                    {
                        levelLoader.PrepareForLoading(demoScene);
                    }
                    break;
            }
        }

        protected async void Pressed()
        {
            pressSound.Play();

            isSelected = !isSelected;
            selector.IsActive = !selector.IsActive;

            await Task.Delay(100);

            isSelected = !isSelected;
            selector.IsActive = !selector.IsActive;
        }
    }
}
