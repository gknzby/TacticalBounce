using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Components;

public class ReflectDummy : Dummy
{
    public override void SetTarget()
    {
        
    }

    public void SetTarget(Vector3 startPoint, Vector3 reflectDir)
    {
        Vector3 target = Vector3.zero;
        RaycastHit hit;
        Debug.DrawRay(startPoint, reflectDir, Color.black);
        if(Physics.Raycast(startPoint, reflectDir, out hit))
        {
            target = hit.point;
        }
        base.SetTarget(target);
    }
}
