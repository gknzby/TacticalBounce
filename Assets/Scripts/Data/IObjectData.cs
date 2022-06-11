using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Data
{
    public interface IObjectData
    {
        void CollectData();
        GameObject CreateObject();
        List<string> GetObjectData();
        void SetObjectData(List<string> objectData);
        int GetID();
        void Preps();
    }
}

