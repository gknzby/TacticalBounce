using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Components
{
    public interface IInputReceiver
    {
        void Click();
        void Release();
        void Drag(Vector2 dragVec);

        void Cancel();
    }
}
