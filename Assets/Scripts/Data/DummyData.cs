using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Components;

namespace TacticalBounce.Data
{
    [System.Serializable]
    public class DummyData : ObjectData
    {
        [SerializeField] private Transform DummyTransform;
        private float reachDistance;

        public override void CollectData()
        {
            base.CollectData();

            reachDistance = this.DummyTransform.GetComponent<Dummy>().GetReachDistance();
        }

        public override void CreateObject(Transform newObject)
        {
            base.CreateObject(newObject);

            DummyTransform.GetComponent<Dummy>().SetReachDistance(reachDistance);
        }

        public override List<string> GetObjectData()
        {
            List<string> dataString = base.GetObjectData();
            dataString.Add(reachDistance.ToString());

            return dataString;
        }

        public override void SetObjectData(List<string> dataString)
        {
            int lastIndex = dataString.Count - 1;
            reachDistance = float.Parse(dataString[lastIndex], System.Globalization.CultureInfo.InvariantCulture);
            dataString.RemoveAt(lastIndex);

            base.SetObjectData(dataString);
        }
    }
}

