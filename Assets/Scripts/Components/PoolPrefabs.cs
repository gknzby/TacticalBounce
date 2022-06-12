using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TacticalBounce
{
    public enum PoolPrefabType
    {
        Ball = 0,
        GoalNet = 1,
        StartDummy = 2,
        AutoDummy = 3,
        RotatableDummy = 4,
        SlidingDummy = 5
    }

    
    public class PoolPrefabs : MonoBehaviour
    {
        private static PoolPrefabs instance = null;
        public static PoolPrefabs Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (PoolPrefabs)FindObjectOfType(typeof(PoolPrefabs));
                    if (instance == null)
                        instance = (new GameObject("PoolObjects")).AddComponent<PoolPrefabs>();
                }
                else
                {
                    if(!Application.isPlaying)
                    {
                        instance = (PoolPrefabs)FindObjectOfType(typeof(PoolPrefabs));
                    }
                }
                return instance;
            }
        }

        [System.Serializable]
        public struct PoolPrefab
        {
            public PoolPrefabType PrefabType;
            public GameObject PrefabObj;
        }
        [SerializeField] List<PoolPrefab> PoolPrefabList = new List<PoolPrefab>();

        public static GameObject GetPoolPrefab(PoolPrefabType pot)
        {
            foreach (PoolPrefab poolPrefab in Instance.PoolPrefabList)
            {
                if (poolPrefab.PrefabType == pot)
                {
                    return poolPrefab.PrefabObj;
                }
            }

            Debug.LogError("Missing pool object type, please add " + pot.ToString() + " to list or don't request it.");
            return null;
        }
    }
}

