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
        private Vector3 targetPos;

        [Min(1f)]
        [SerializeField] protected float ReachDistance = 0;

        [Min(1f)]
        [SerializeField] protected float OutReachFall = 1;

        [SerializeField] private GameObject BallPath_Obj;

        public abstract bool CalculatePath(Ray inRay, RaycastHit inHit, out Ray outRay, out RaycastHit outHit);

        public virtual void SetTarget(Vector3 inHit, Vector3 targetHit, bool inReach)
        {
            if(inReach)
            {
                this.targetPos = targetHit;
                DrawPath(inHit, this.targetPos, inReach);
            }
            else
            {
                Vector3 outReachDir = (targetHit - inHit);
                outReachDir.y = 0;
                outReachDir = outReachDir.normalized;
                this.targetPos = inHit + outReachDir * this.OutReachFall;
                DrawPath(inHit, inHit + (outReachDir * this.ReachDistance), inReach);
            }
            
        }

        protected virtual void DrawPath(Vector3 startPoint, Vector3 endPoint, bool inReach)
        {
            BallPath_Obj.SetActive(true);
            BallPath_Obj.GetComponent<BallPath>().DrawPath(startPoint, endPoint, inReach);
        }
        public virtual void ClearPath()
        {
            BallPath_Obj.SetActive(false);
        }

        public virtual Vector3 GetTarget()
        {
            return targetPos;
        }
    }
}

