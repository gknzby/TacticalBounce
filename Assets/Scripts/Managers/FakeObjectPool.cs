using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TacticalBounce.Managers
{

    /*
     * I made a fake object pooling, because this gives me
     * to be able to add Object Pool more easily in the future
     * instead of this.
     */
    public class FakeObjectPool : MonoBehaviour, IObjectPool
    {
        #region IObjectPool
        public string ManagerType { get; set; }

        public GameObject GetPoolObject(GameObject poolObjPrefab)
        {
            return Instantiate(poolObjPrefab);
        }

        public void ReturnPoolObject(GameObject poolObj)
        {
            Destroy(poolObj);
        }
        #endregion

        #region Unity Functions
        private void Awake()
        {
            ManagerType = "ObjectPool";
            ManagerProvider.AddManager(this);
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager(this);
        }
        #endregion
    }
}