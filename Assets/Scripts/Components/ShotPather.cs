using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Components
{
    [RequireComponent(typeof(Dummy))]
    public class ShotPather : MonoBehaviour, IInputReceiver
    {
        #region InputReceiver
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
            ClearPath();
            Managers.GameManager.Instance.Shotted();
        }
        #endregion

        private Dictionary<Transform, int> hittedDummies = new Dictionary<Transform, int>();

        private void ClearPath()
        {
            foreach(KeyValuePair<Transform, int> hitDummy in hittedDummies)
            {
                hitDummy.Key.GetComponent<Dummy>().ClearPath();
            }
        }

        private bool TryAddHittedDummy(Transform dummyTransform)
        {
            if (hittedDummies.TryAdd(dummyTransform, 0))
            {
                dummyTransform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ClearHittedDummies()
        {
            ClearPath();

            foreach (KeyValuePair<Transform, int> hitDummy in hittedDummies)
            {
                hitDummy.Key.gameObject.layer = LayerMask.NameToLayer("Default");
            }

            hittedDummies.Clear();
        }

        private void CalculatePath()
        {
            ClearHittedDummies();

            Ray inRay = new Ray(transform.position, transform.forward);
            RaycastHit inHit = new RaycastHit();
            Ray outRay;
            RaycastHit outHit;

            Dummy dummy = this.GetComponent<Dummy>();
            hittedDummies.Add(dummy.transform, 0);

            for(int i = 0; i < 10; i++)
            {
                if(dummy.CalculatePath(inRay, inHit, out outRay, out outHit))
                {
                    if(outHit.transform.CompareTag("Dummy"))
                    {
                        if(TryAddHittedDummy(outHit.transform))
                        {
                            dummy.SetTarget(outRay.origin, outHit.point, true);

                            dummy = outHit.transform.GetComponent<Dummy>();
                            inRay = outRay;
                            inHit = outHit;
                            continue;
                        }
                    }
                    else if(outHit.transform.CompareTag("Goal"))
                    {
                        dummy.SetTarget(outRay.origin, outHit.point, true);
                    }
                    else
                    {
                        dummy.SetTarget(outRay.origin, outHit.point, false);
                    }
                }
                else
                {
                    dummy.SetTarget(outRay.origin, outRay.origin + outRay.direction, false);
                }
                break;
            }
        }
    }
}

