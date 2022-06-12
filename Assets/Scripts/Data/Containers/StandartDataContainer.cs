using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Data
{
    public class StandartDataContainer : ObjectDataContainer
    {
        public override IObjectData DataObject { get { return dataObject; } set { dataObject = value as ObjectData; } }

        [SerializeField] private ObjectData dataObject = new ObjectData();
    }
}
