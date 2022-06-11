using System.Collections;
using System.Collections.Generic;
using TacticalBounce.Components;
using UnityEngine;


namespace TacticalBounce.Managers
{
    public class UIManager : MonoBehaviour, IUIManager
    {
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
        #endregion

        #region Class Functions
        private void StartGame()
        {
            IGameManager igm = ManagerProvider.GetManager("GameManager") as IGameManager;
            igm.SendGameAction(GameAction.StartGame);
        }
        #endregion

        #region Unity Functions
        private void Awake()
        {
            ManagerType = "UIManager";
            ManagerProvider.AddManager(this);
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager(this);
        }
        #endregion
    }
}

