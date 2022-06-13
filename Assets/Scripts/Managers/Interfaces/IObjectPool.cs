using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Managers
{
    public interface IObjectPool : IManager
    {
        GameObject GetPoolObject(GameObject poolObjPrefab);
        GameObject GetPoolObject(PoolPrefabType poolPrefabType);
        void ReturnPoolObject(GameObject poolObj);
    }
}

