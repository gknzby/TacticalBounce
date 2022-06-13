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
        public string ManagerType { get { return "ObjectPool"; } }

        public GameObject GetPoolObject(GameObject poolObjPrefab)
        {
            return Instantiate(poolObjPrefab, this.transform);
        }

        public GameObject GetPoolObject(PoolPrefabType poolPrefabType)
        {
            return this.GetPoolObject(PoolPrefabs.GetPoolPrefab(poolPrefabType));
        }

        public void ReturnPoolObject(GameObject poolObj)
        {
            Destroy(poolObj);
        }
        #endregion

        #region Unity Functions => Awake, OnDestroy
        private void Awake()
        {
            ManagerProvider.AddManager(this);
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager(this);
        }
        #endregion
    }
}