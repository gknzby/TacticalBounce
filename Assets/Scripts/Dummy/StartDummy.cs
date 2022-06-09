using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TacticalBounce.Components
{
    public class StartDummy : Dummy
    {
        public override void SetTarget()
        {
            RaycastHit hit;
            Vector3 targetVec = Vector3.zero;
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit))
            {
                targetVec = hit.point;
            }

            base.SetTarget(targetVec);
        }

        public override Vector3 GetTarget()
        {
            this.SetTarget();
            return base.GetTarget();
        }

    }

}
