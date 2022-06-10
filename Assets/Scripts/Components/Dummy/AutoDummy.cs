using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Components
{

    public class AutoDummy : Dummy
    {
        [SerializeField] private Transform Target_T;


        public override bool CalculatePath(Ray inRay, RaycastHit inHit, out Ray outRay, out RaycastHit outHit)
        {            
            Vector3 outDir = (Target_T.position - inHit.point).normalized;
            outDir.y = 0;
            outRay = new Ray(inHit.point, outDir);
            

            return Physics.Raycast(outRay, out outHit);
        }
    }
}
