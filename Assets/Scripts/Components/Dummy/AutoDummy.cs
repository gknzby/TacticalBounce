using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Components
{

    public class AutoDummy : Dummy
    {
        //Inspector Variable
        [SerializeField] private Vector3 TargetPosition;

        //Dummy Override
        public override bool CalculatePath(Ray inRay, RaycastHit inHit, out Ray outRay, out RaycastHit outHit)
        {            
            Vector3 outDir = (TargetPosition - inHit.point).normalized;
            outDir.y = 0;
            outRay = new Ray(inHit.point, outDir);
            

            return Physics.Raycast(outRay, out outHit);
        }

        //Class Specific Functions (These for AutoDummyData)
        public Vector3 GetTargetPosition()
        {
            return TargetPosition;
        }
        public void SetTargetPosition(Vector3 TargetPosition)
        {
            this.TargetPosition = TargetPosition;
        }

        //Awake, OnDrawGizmos, OnApplicationQuit
        #region Gizmos
#if UNITY_EDITOR
        private void Awake()
        {
            hideGizmos = true;
        }

        [Header("Gizmos")]
        [SerializeField] private bool hideGizmos = false;
        private void OnDrawGizmos()
        {
            if (hideGizmos) 
                return;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.transform.position, TargetPosition);
        }

        private void OnApplicationQuit()
        {
            hideGizmos = false;
        }
#endif
        #endregion
    }
}
