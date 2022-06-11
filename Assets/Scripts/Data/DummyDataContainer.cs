using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Data
{
    public class DummyDataContainer : MonoBehaviour, IObjectDataContainer
    {
        public IObjectData DataObject { get { return dataObject; } set { dataObject = value as DummyData; } }

        [SerializeField] private DummyData dataObject;
    }
}

