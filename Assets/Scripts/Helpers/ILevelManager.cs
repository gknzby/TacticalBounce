using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Managers
{
    public interface ILevelManager : IManager
    {
        int LevelCount { get; }
        void LoadLevel(int index);
        void SaveLevel();
        void NextLevel();
        void RetryLevel();
    }
}