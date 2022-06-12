using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Managers
{
    public interface IObjectPool : IManager
    {
        GameObject GetPoolObject(GameObject poolObjPrefab);
        void ReturnPoolObject(GameObject poolObj);
    }
}

