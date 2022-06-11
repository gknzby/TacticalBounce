using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Components;

namespace TacticalBounce.Data
{
    [System.Serializable]
    public class AutoDummyData : ObjectData
    {
        [SerializeField] private Transform DummyTransform;
        private Vector3 TargetPosition;

        public override void CollectData()
        {
            base.CollectData();

            TargetPosition = this.DummyTransform.GetComponent<AutoDummy>().GetTargetPosition();
        }

        public override GameObject CreateObject()
        {
            GameObject createdObj = base.CreateObject();

            createdObj.GetComponent<AutoDummy>().SetTargetPosition(TargetPosition);

            return createdObj;
        }
    }
}
