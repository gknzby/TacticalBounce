using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Components;

public class ReflectDummy : Dummy
{

    //public void SetTarget(Vector3 startPoint, Vector3 reflectDir)
    //{
    //    Vector3 target = Vector3.zero;
    //    RaycastHit hit;
    //    if(Physics.Raycast(startPoint, reflectDir, out hit))
    //    {
    //        target = hit.point;
    //    }
    //    base.SetTarget(target);
    //}

    //public override void SetTarget(Vector3 startPoint, Vector3 hitPoint, out Vector3 reflectDir)
    //{
    //    Vector3 inDirect = (hitPoint - startPoint).normalized;
    //    Vector3 hitNormal = this.transform.forward;
    //    RaycastHit hit;
    //    if(Physics.Raycast(startPoint, inDirect, out hit))
    //    {
    //        hitNormal = hit.normal;
    //    }

    //    reflectDir = Vector3.Reflect(inDirect, hitNormal);

    //    Vector3 target = Vector3.zero;        
    //    if (Physics.Raycast(hit.point, reflectDir, out hit))
    //    {
    //        target = hit.point;
    //    }
    //    base.SetTarget(target);
    //}

    public override bool CalculatePath(Ray inRay, RaycastHit inHit, out Ray outRay, out RaycastHit outHit)
    {
        Vector3 outDir = Vector3.Reflect(inRay.direction, inHit.normal);
        outRay = new Ray(inHit.point, outDir);

        return Physics.Raycast(outRay, out outHit, ReachDistance);
    }
}
