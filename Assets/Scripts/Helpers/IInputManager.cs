using System.Collections;
using System.Collections.Generic;
using TacticalBounce.Components;
using UnityEngine;

namespace TacticalBounce.Managers
{
    public interface IInputManager : IManager
    {
        void SetDefaultReceiver(IInputReceiver inputReceiver);
}

}
