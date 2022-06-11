using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TacticalBounce.Data
{
    public class ObjectDataContainer : MonoBehaviour, IObjectDataContainer
    {
        public IObjectData DataObject { get { return dataObject; } set { dataObject = value as ObjectData; } }

        [SerializeField] private ObjectData dataObject;
    }
}
