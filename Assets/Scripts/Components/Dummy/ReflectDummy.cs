using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Components;


namespace TacticalBounce.Components
{
    public class ReflectDummy : Dummy
    {
        public override bool CalculatePath(Ray inRay, RaycastHit inHit, out Ray outRay, out RaycastHit outHit)
        {
            Vector3 outDir = Vector3.Reflect(inRay.direction, inHit.normal);
            outRay = new Ray(inHit.point, outDir);

            return Physics.Raycast(outRay, out outHit, ReachDistance);
        }

#if UNITY_EDITOR
        private void Awake()
        {
            hideGizmos = true;
        }

        [Header("Gizmos")]
        [SerializeField] private bool hideGizmos = false;
        private void OnDrawGizmos()
        {
            if (hideGizmos) return;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, base.ReachDistance);
        }

        private void OnApplicationQuit()
        {
            hideGizmos = false;
        }
#endif
    }
}

