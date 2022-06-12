using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Managers;

namespace TacticalBounce.Components
{
    public enum UIAction
    {
        StartGame = 0,
        NextLevel = 7,
        RetryLevel = 1,
        LoadLevel = 2,
        RestartGame = 3,
        ShowMenu = 4,
        ResumeGame = 5,
        PauseGame = 6
    }
    public class UIComponent : MonoBehaviour
    {
        public UIAction uiAction;
        public string value;

        public void SendToUIManager()
        {
            IUIManager uiMng = ManagerProvider.GetManager("UIManager") as IUIManager;

            uiMng.SendUIComponent(this);
        }
    }
}

