using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Managers;

namespace TacticalBounce.Components
{
    public enum UIAction
    {
        StartGame,
        ShowMainMenu
    }
    public class UIComponent : MonoBehaviour
    {
        public UIAction uiAction;

        public void SendToUIManager()
        {
            IUIManager uiMng = ManagerProvider.GetManager("UIManager") as IUIManager;

            uiMng.SendUIComponent(this);
        }
    }
}

