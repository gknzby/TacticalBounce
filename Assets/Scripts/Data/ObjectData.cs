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
        [SerializeField] protected GameObject ObjectPrefab;
        #endregion

        #region Class Variables
        private int objectID = 0;
        private GameObject objectPrefab;

        private Vector3 objectPosition;
        private Quaternion objectRotation;
        private Vector3 objectScale;

        private GameObject createdObj;
        #endregion

        #region IObjectData
        public virtual void CollectData()
        {
            CollectTransformData();
            CollectPrefabData();
        }

        public virtual GameObject CreateObject()
        {
            IObjectPool iop = ManagerProvider.GetManager("ObjectPool") as IObjectPool;
            createdObj = iop.GetPoolObject(objectPrefab);

            createdObj.transform.position = objectPosition;
            createdObj.transform.rotation = objectRotation;
            createdObj.transform.localScale = objectScale;

            return createdObj;
        }

        public virtual int GetID()
        {
            if(objectID == 0)
            {
                objectID = this.ObjectTransform.GetInstanceID();
            }

            return objectID;
        }

        public virtual void Preps()
        {
            objectID = this.GetID();
        }

        //Later
        public virtual List<string> GetObjectData()
        {
            return null;
        }
        //Later
        public virtual void SetObjectData(List<string> objectData)
        {
            return;
        }
        #endregion

        #region Class Functions
        private void CollectTransformData()
        {
            Debug.LogError("To Collect object data, assign a Transform to ObjectData");

            objectPosition = ObjectTransform.position;
            objectRotation = ObjectTransform.rotation;
            objectScale = ObjectTransform.localScale;
        }
        
        private void CollectPrefabData()
        {
            Debug.LogError("To Collect object data, assign a Prefab to ObjectData");

            objectPrefab = ObjectPrefab;
        }
        #endregion
    }
}

