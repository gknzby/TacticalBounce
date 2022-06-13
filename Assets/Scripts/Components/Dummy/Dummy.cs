using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Components
{
    /*
     * Dependency notes: Uses BallPath class to draw path between its and target
     */
    [System.Serializable]
    public abstract class Dummy : MonoBehaviour
    {
        #region Inspector Variables
        [Min(1f)]
        [SerializeField] protected float ReachDistance = 10;
        [SerializeField] private GameObject BallPath_Obj;
        #endregion

        #region Class Variables
        private Vector3 targetPos;
        protected float OutReachFall = 5f;
        #endregion

        #region Class Functions
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

        public virtual void ClearPath()
        {
            BallPath_Obj.SetActive(false);
        }

        public virtual Vector3 GetTarget()
        {
            return targetPos;
        }

        public virtual float GetReachDistance()
        {
            return ReachDistance;
        }

        public virtual void SetReachDistance(float distance)
        {
            ReachDistance = distance;
        }

        protected virtual void DrawPath(Vector3 startPoint, Vector3 endPoint, bool inReach)
        {
            BallPath_Obj.SetActive(true);
            BallPath_Obj.GetComponent<BallPath>().DrawPath(startPoint, endPoint, inReach);
        }
        #endregion
    }
}

