using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Data
{
    public interface IObjectData
    {
        void CollectData();
        void CreateObject(Transform newObject);
        List<string> GetObjectData();
        GameObject GetPrefab();
        void SetObjectData(List<string> objectData);
    }
}

