using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Components;

namespace TacticalBounce.Data
{
    [System.Serializable]
    public class AutoDummyData : ObjectData
    {
        #region Inspector&Class Variables
        [SerializeField] private Transform DummyTransform;
        private Vector3 targetPosition;
        #endregion

        #region ObjectData
        public override void CollectData()
        {
            base.CollectData();

            targetPosition = this.DummyTransform.GetComponent<AutoDummy>().GetTargetPosition();
        }

        public override void CreateObject(Transform newObject)
        {
            base.CreateObject( newObject );

            DummyTransform.GetComponent<AutoDummy>().SetTargetPosition(targetPosition);
        }

        public override List<string> GetObjectData()
        {
            List<string> dataString = base.GetObjectData();
            dataString.Add(targetPosition.ToString());

            return dataString;
        }

        public override void SetObjectData(List<string> dataString)
        {
            int lastIndex = dataString.Count - 1;
            targetPosition = base.StringToVector3(dataString[lastIndex]);
            dataString.RemoveAt(lastIndex);

            base.SetObjectData(dataString);
        }
        #endregion
    }
}
