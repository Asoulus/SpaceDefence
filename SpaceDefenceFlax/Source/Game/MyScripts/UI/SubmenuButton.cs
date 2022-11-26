using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game
{
    public class SubmenuButton : Script
    {

        public Actor submenu = null;

        public int id;

        public Actor selector;

        public SceneReference scene;

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

            SubmenuManager.GetInstance().onMainMenuButtonPressed += MainMenuButtonPressed;

            submenu.IsActive = false;
        }

        private void MainMenuButtonPressed(int value)
        {
            if (id == value)
            {
                submenu.IsActive = true;
            }
            else
            {
                submenu.IsActive = false;
            }
        }

        public void ToggleSubmenu()
        {
            SubmenuManager.GetInstance().MainMenuButtonPressed(id);
        }

        public override void OnDisable()
        {
            SubmenuManager.GetInstance().onMainMenuButtonPressed -= MainMenuButtonPressed;
        }   

        protected void ButtonPressed()
        {
            Pressed();

            switch (buttonType)
            {
                case "exit":
                    {
                        Engine.RequestExit();
                    }break;
                case "return":
                    {
                        SubmenuManager.GetInstance().MainMenuButtonPressed(-1);
                    }
                    break;
            }
        }

        protected async void Pressed()
        {
            isSelected = !isSelected;
            selector.IsActive = !selector.IsActive;

            await Task.Delay(100);

            isSelected = !isSelected;
            selector.IsActive = !selector.IsActive;

            ToggleSubmenu();
        }

        protected void LoadScene()
        {
            if (scene != null)
            {
                //levelLoader.LoadScene(scene);
                Debug.Log("SCENE!");
            }
        }
    }
}
