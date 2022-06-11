using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Data
{
    public class AutoDummyDataContainer : MonoBehaviour, IObjectDataContainer
    {
        public IObjectData DataObject { get { return dataObject; } set { dataObject = value as AutoDummyData; } }

        [SerializeField] private AutoDummyData dataObject;
    }
}
