using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Managers;


namespace TacticalBounce.Data
{
    [System.Serializable]
    public class ObjectData : IObjectData
    {
        #region Inspector Variables
        [SerializeField] protected Transform ObjectTransform;
        [SerializeField] protected PoolPrefabType poolObjectType;
        #endregion

        #region Class Variables
        private Vector3 objectPosition;
        private Quaternion objectRotation;
        private Vector3 objectScale;

        private GameObject createdObj;
        #endregion

        #region IObjectData
        public virtual void CollectData()
        {
            Debug.Assert(ObjectTransform != null, "To Collect object data, assign a Transform to ObjectData");

            objectPosition = ObjectTransform.position;
            objectRotation = ObjectTransform.rotation;
            objectScale = ObjectTransform.localScale;
        }

        public virtual void CreateObject(Transform newObject)
        {
            newObject.position = objectPosition;
            newObject.rotation = objectRotation;
            newObject.localScale = objectScale;
        }

        public virtual List<string> GetObjectData()
        {
            List<string> dataString = new List<string>();
            dataString.Add(poolObjectType.ToString());
            dataString.Add(objectPosition.ToString());
            dataString.Add(objectRotation.ToString());
            dataString.Add(objectScale.ToString());

            return dataString;            
        }

        public virtual void SetObjectData(List<string> dataString)
        {
            poolObjectType = (PoolPrefabType) System.Enum.Parse(typeof(PoolPrefabType), dataString[0]);
            objectPosition = StringToVector3(dataString[1]);
            objectRotation = StringToQuartenion(dataString[2]);
            objectScale = StringToVector3(dataString[3]);

            return;
        }

        public virtual GameObject GetPrefab()
        {
            return PoolPrefabs.GetPoolPrefab(poolObjectType);
        }
        #endregion

        #region Class Functions
        protected Vector3 StringToVector3(string sVector)
        {
            // Remove the parentheses
            if (sVector.StartsWith("(") && sVector.EndsWith(")"))
            {
                sVector = sVector.Substring(1, sVector.Length - 2);
            }

            // split the items
            string[] sArray = sVector.Split(',');

            // store as a Vector3
            Vector3 result = new Vector3(
                float.Parse(sArray[0], System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(sArray[1], System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(sArray[2], System.Globalization.CultureInfo.InvariantCulture));

            return result;
        }

        protected Quaternion StringToQuartenion(string sVector)
        {
            // Remove the parentheses
            if (sVector.StartsWith("(") && sVector.EndsWith(")"))
            {
                sVector = sVector.Substring(1, sVector.Length - 2);
            }

            // split the items
            string[] sArray = sVector.Split(',');

            // store as a Quartenion
            Quaternion result = new Quaternion(
                float.Parse(sArray[0], System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(sArray[1], System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(sArray[2], System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(sArray[3], System.Globalization.CultureInfo.InvariantCulture));

            return result;
        }
        #endregion
    }
}

