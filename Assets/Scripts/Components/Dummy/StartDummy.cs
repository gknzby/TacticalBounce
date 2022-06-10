using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TacticalBounce.Components
{
    public class StartDummy : Dummy
    {
        //public override Vector3 GetTarget()
        //{
        //    Debug.Log(base.GetTarget());
        //    return base.GetTarget();
        //}
        public override bool CalculatePath(Ray inRay, RaycastHit inHit, out Ray outRay, out RaycastHit outHit)
        {
            outRay = new Ray(this.transform.position, this.transform.forward);

            return Physics.Raycast(outRay, out outHit, base.ReachDistance); //CalculatePath(outRay, out outHit);
        }
        //private bool CalculatePath(Ray ray, out RaycastHit hit)
        //{
        //    return Physics.Raycast(ray, out hit, base.ReachDistance);
        //}

        //private void SetTarget()
        //{
        //    Ray ray = new Ray(this.transform.position, this.transform.forward);
        //    RaycastHit hit;
            
        //    bool inReach = CalculatePath(ray, out hit);
        //    SetTarget(ray.direction, inReach);
        //}

    }

}
