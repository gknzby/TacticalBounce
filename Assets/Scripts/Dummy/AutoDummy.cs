using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Components
{

    public class AutoDummy : Dummy
    {
        [SerializeField] private Transform Target_T;

        public override void SetTarget()
        {
            base.SetTarget(Target_T.position);
        }
    }
}
