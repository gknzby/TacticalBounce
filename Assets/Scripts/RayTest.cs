using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Components;

public class RayTest : MonoBehaviour
{
    private void FixedUpdate()
    {
        Vector3 currentPoint = transform.position;
        Vector3 nextTarget = transform.forward;
        RaycastHit hit;
        for(int i = 0; i < 10; i++)
        {
            if (Physics.Raycast(currentPoint, nextTarget, out hit))
            {
                Debug.DrawLine(currentPoint, hit.point);
                if (hit.transform.CompareTag("Dummy"))
                {
                    Dummy dummy = hit.transform.GetComponent<Dummy>();
                    switch (dummy.DType)
                    {
                        case DummyType.Auto:
                            dummy.SetTarget();
                            nextTarget = dummy.GetTarget() - hit.point;
                            break;
                        case DummyType.Reflect:
                            nextTarget = Vector3.Reflect((hit.point - currentPoint).normalized, hit.normal); //InDirection and Surface Normal
                            ((ReflectDummy)dummy).SetTarget(hit.point, nextTarget);
                            break;
                        default:
                            break;
                    }
                    currentPoint = hit.point;
                    nextTarget.y = 0;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

    }



    #region DebugRay
    [ContextMenu("Start Draw")]
    private void StartDraw()
    {
        StartCoroutine(DrawLine());
    }

    private IEnumerator DrawLine()
    {
        while(true)
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            yield return null;
        }
    }

    [ContextMenu("Stop Draw")]
    private void StopDraw()
    {
        StopAllCoroutines();
    }
    #endregion
}
