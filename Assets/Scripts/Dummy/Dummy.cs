using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Components
{
    public enum DummyType
    {
        Auto,
        Reflect,
        Start
    }

    [System.Serializable]
    public abstract class Dummy : MonoBehaviour
    {
        [SerializeField] private DummyType dType;
        public DummyType DType { get => dType; private set => dType = value; }

        protected Vector3 targetPos;


        public abstract void SetTarget();

        public void SetTarget(Vector3 targetPos)
        {
            this.targetPos = targetPos;
        }
        public virtual Vector3 GetTarget()
        {
            return targetPos;
        }
    }
}

