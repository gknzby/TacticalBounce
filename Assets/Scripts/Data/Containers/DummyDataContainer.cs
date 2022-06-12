using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Data
{
    public class DummyDataContainer : ObjectDataContainer
    {
        public override IObjectData DataObject { get { return dummyData; } set { dummyData = value as DummyData; } }

        [SerializeField] private DummyData dummyData = new DummyData();
    }
}

