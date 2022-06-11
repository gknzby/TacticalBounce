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
        public string ManagerType { get; set; }

        public void SendUIAction(UIAction uiAction)
        {
            switch (uiAction)
            {
                case UIAction.StartGame:
                    StartGame();
                    break;
                default:
                    break;
            }
        }

        public void SendUIComponent(UIComponent uiComponent)
        {
            //Extensionable
            //UI Component could has more variable in it
            this.SendUIAction(uiComponent.uiAction);
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
        #endregion

        #region Class Functions
        private void StartGame()
        {
            IGameManager igm = ManagerProvider.GetManager("GameManager") as IGameManager;
            igm.SendGameAction(GameAction.StartGame);
        }

        //private void HandleGameStateChange(Managers.GameState newState)
        //{
        //    switch (newState)
        //    {
        //        case GameState.Menu:
        //            break;
        //        case GameState.Preparation:
        //            break;
        //        case GameState.Shotted:
        //            break;
        //        case GameState.Goal:
        //            break;
        //        case GameState.Loose:
        //            break;
        //        default:
        //            break;
        //    }
        //}
        #endregion

        #region Unity Functions
        private void Awake()
        {
            ManagerType = "UIManager";
            uiMenus = new List<IUIMenu>();
            ManagerProvider.AddManager(this);
        }

        private void Start()
        {
            
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager(this);
        }
        #endregion
    }
}

