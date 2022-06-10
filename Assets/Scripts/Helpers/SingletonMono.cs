using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Helpers
{
    public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; protected set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(this.gameObject);
            }

            Debug.Log("Base");
        }


        private void OnApplicationQuit()
        {
            Instance = null;
            Destroy(this.gameObject);
        }
    }
}