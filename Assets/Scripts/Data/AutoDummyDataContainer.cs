using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Data
{
    public class AutoDummyDataContainer : ObjectDataContainer
    {
        public override IObjectData DataObject { get { return autoDummyData; } set { autoDummyData = value as AutoDummyData; } }

        [SerializeField] private AutoDummyData autoDummyData = new AutoDummyData();
    }
}
