using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Managers
{
    public interface IUIManager : IManager
    {
        void SendUIComponent(Components.UIComponent uiComponent);
        void SendUIAction(UIAction uiAction);
        void RegisterMenu(IUIMenu uiMenu);
        void ShowMenu(string menuName);
        void HideMenu(string menuName);
    }
}

