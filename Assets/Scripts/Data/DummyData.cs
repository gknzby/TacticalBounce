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

        public override GameObject CreateObject()
        {
            GameObject createdObj =  base.CreateObject();

            Dummy dummy = FindDummy(createdObj.transform);
            dummy.SetReachDistance(reachDistance);

            return createdObj;
        }


        private Dummy FindDummy(Transform tf)
        {
            if(tf.GetComponent<Dummy>() != null)
            {
                return tf.GetComponent<Dummy>();
            }

            Queue<Transform> searchQueue = new Queue<Transform>();
            searchQueue.Enqueue(tf);

            while(searchQueue.Count == 0)
            {
                Transform searching = searchQueue.Dequeue();
                for(int i = 0; i < searching.childCount; i++)
                {
                    if(searching.GetChild(i).GetComponent<Dummy>() != null)
                    {
                        return searching.GetChild(i).GetComponent<Dummy>();
                    }
                    else if(0 < searching.GetChild(i).childCount)
                    {
                        searchQueue.Enqueue(searching.GetChild(i));
                    }
                }
            }

            return null;
        }
    }
}

