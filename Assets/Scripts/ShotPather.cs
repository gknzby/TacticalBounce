using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Components
{
    public class ShotPather : MonoBehaviour, IInputReceiver
    {
        public void Click()
        {
            return;
        }

        public void Drag(Vector2 dragVec)
        {
            this.transform.Rotate(Vector3.up, dragVec.x);
            CalculatePath();
        }

        public void Release()
        {
            Managers.GameManager.Instance.Shotted();
        }

        private void CalculatePath()
        {
            Vector3 currentPoint = transform.position;
            Vector3 nextTarget = transform.forward;
            RaycastHit hit;
            for (int i = 0; i < 10; i++)
            {
                if (Physics.Raycast(currentPoint, nextTarget, out hit))
                {
                    DrawPath(currentPoint, hit.point);
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

        private void DrawPath(Vector3 startPoint, Vector3 endPoint)
        {
            Debug.DrawLine(startPoint, endPoint);
        }
    }
}

