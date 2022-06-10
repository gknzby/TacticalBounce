using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Managers
{
    public class ObjectPool : Helpers.SingletonMono<ObjectPool>
    {
        public enum ObjType
        {
            None,
            Dummy
        }
        [System.Serializable]
        public class PoolObj
        {
            public GameObject Obj;
            public ObjType ObjType;
            public int PoolCount;
            public List<GameObject> PassiveObjs = new List<GameObject>();
        }
        public List<PoolObj> pools = new List<PoolObj>();

        protected override void Awake()
        {      
            base.Awake();

            CreatePool();
        }

        private void CreatePool()
        {
            for (int i = 0; i < pools.Count; i++)
            {
                for (int j = 0; j < pools[i].PoolCount; j++)
                {
                    pools[i].PassiveObjs.Add(GameObject.Instantiate(pools[i].Obj, this.transform));
                }
            }
        }

        public void ReturnObject(GameObject gObj, ObjType objType)
        {
            gObj.SetActive(false);
            for (int i = 0; i < pools.Count; i++)
            {
                if (pools[i].ObjType == objType)
                {
                    if (pools[i].PassiveObjs.Count < pools[i].PoolCount)
                        pools[i].PassiveObjs.Add(gObj);
                    else
                        GameObject.Destroy(gObj);

                }
            }
        }

        public GameObject GetObject(ObjType objType)
        {
            if (objType == ObjType.None)
                return null;
            GameObject gObj = null;
            for (int i = 0; i < pools.Count; i++)
            {
                if (pools[i].ObjType == objType)
                {
                    if (pools[i].PassiveObjs.Count > 0)
                    {
                        gObj = pools[i].PassiveObjs[0];
                        pools[i].PassiveObjs.Remove(gObj);
                    }
                    else
                    {
                        gObj = GameObject.Instantiate(pools[i].Obj, this.transform);
                    }
                }
            }
            return gObj;
        }
    }
}