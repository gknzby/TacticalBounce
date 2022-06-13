using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TacticalBounce.Managers
{
    public interface IGameManager : IManager
    {
        void SendGameAction(GameAction gameAction);
    }
}

