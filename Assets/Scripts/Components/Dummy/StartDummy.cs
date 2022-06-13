using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TacticalBounce.Components
{
    public class StartDummy : Dummy
    {
        //Dummy Override
        public override bool CalculatePath(Ray inRay, RaycastHit inHit, out Ray outRay, out RaycastHit outHit)
        {
            outRay = new Ray(this.transform.position, this.transform.forward);

            return Physics.Raycast(outRay, out outHit, base.ReachDistance); //CalculatePath(outRay, out outHit);
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
            if (hideGizmos) return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(this.transform.position, base.ReachDistance);
        }

        private void OnApplicationQuit()
        {
            hideGizmos = false;
        }
#endif
        #endregion 
    }

}
