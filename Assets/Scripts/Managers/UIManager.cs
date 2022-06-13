using System;
using System.Collections;
using System.Collections.Generic;
using TacticalBounce.Components;
using UnityEngine;


namespace TacticalBounce.Managers
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        private List<IUIMenu> uiMenus;

        #region IUIManager
        public string ManagerType { get { return "UIManager"; } }

        public void SendUIAction(UIAction uiAction)
        {
            switch (uiAction)
            {
                case UIAction.StartGame:
                    LoadLevel();
                    break;
                case UIAction.RetryLevel:
                    LoadLevel();
                    break;
                case UIAction.NextLevel:
                    NextLevel();
                    break;
                case UIAction.RestartGame:
                    RestartGame();
                    break;
                case UIAction.ResumeGame:
                    ResumeGame();
                    break;
                case UIAction.PauseGame:
                    StopGame();
                    break;
                default:
                    break;
            }
        }

        public void SendUIComponent(UIComponent uiComponent)
        {
            switch (uiComponent.uiAction)
            {
                case UIAction.LoadLevel:
                    int level = int.Parse(uiComponent.value);
                    LoadLevel(level);
                    HideMenu("LevelsMenu");
                    break;
                case UIAction.ShowMenu:
                    ShowMenu(uiComponent.value);
                    break;
                default:
                    SendUIAction(uiComponent.uiAction);
                    break;
            }
        }

        public void RegisterMenu(IUIMenu uiMenu)
        {
            if(!uiMenus.Contains(uiMenu))
            {
                uiMenus.Add(uiMenu);
            }
            uiMenu.HideMenu();
        }
        public void ShowMenu(string menuName)
        {
            foreach(IUIMenu uiMenu in uiMenus)
            {
                if(uiMenu.GetMenuName() == menuName)
                {
                    uiMenu.ShowMenu();
                    return;
                }
            }

            Debug.LogWarning("UIManager couldn't find " + menuName);
        }
        public void HideMenu(string menuName)
        {
            foreach (IUIMenu uiMenu in uiMenus)
            {
                if (uiMenu.GetMenuName() == menuName)
                {
                    uiMenu.HideMenu();
                    return;
                }
            }

            Debug.LogWarning("UIManager couldn't find " + menuName);
        }
        #endregion

        #region Class Functions
        private void ResumeGame()
        {
            IGameManager igm = ManagerProvider.GetManager("GameManager") as IGameManager;
            igm.SendGameAction(GameAction.ResumeGame);

            this.HideMenu("PauseMenu");
        }
        private void StopGame()
        {
            IGameManager igm = ManagerProvider.GetManager("GameManager") as IGameManager;
            igm.SendGameAction(GameAction.PauseGame);

            this.ShowMenu("PauseMenu");
        }
        private void RestartGame()
        {
            IGameManager igm = ManagerProvider.GetManager("GameManager") as IGameManager;
            igm.SendGameAction(GameAction.Restart);
        }
        private void LoadLevel()
        {
            if(!PlayerPrefs.HasKey("Level"))
            {
                PlayerPrefs.SetInt("Level", 0);
            }

            IGameManager igm = ManagerProvider.GetManager("GameManager") as IGameManager;
            igm.SendGameAction(GameAction.LoadLevel);
        }
        private void LoadLevel(int index)
        {
            PlayerPrefs.SetInt("Level", index);

            LoadLevel();
        }
        private void NextLevel()
        {
            if(!PlayerPrefs.HasKey("Level"))
            {
                Debug.LogError("Next level but there is no 'Level' key in PlayerPrefs");
                PlayerPrefs.SetInt("Level", 0);
            }

            LoadLevel(PlayerPrefs.GetInt("Level") + 1);
        }
        #endregion

        #region Unity Functions => Awake, OnDestroy
        private void Awake()
        {
            uiMenus = new List<IUIMenu>();
            ManagerProvider.AddManager(this);
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager(this);
        }
        #endregion
    }
}

