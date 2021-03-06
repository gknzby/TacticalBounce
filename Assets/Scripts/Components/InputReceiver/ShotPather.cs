using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Managers;

namespace TacticalBounce.Components
{
    /*
     * Dependency notes: Calls InputManager.SetDefault() in OnEnable()
     */
    [RequireComponent(typeof(Dummy))]
    public class ShotPather : MonoBehaviour, IInputReceiver
    {
        private Dictionary<Transform, int> hittedDummies = new Dictionary<Transform, int>();

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
            IGameManager igm = ManagerProvider.GetManager("GameManager") as IGameManager;
            if (igm != null)
            {
                igm.SendGameAction(GameAction.Shot);
            }
            this.GetComponent<Collider>().enabled = true;
        }

        public void Cancel()
        {
            ClearPath();
        }
        #endregion

        #region Class Functions
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
        #endregion

        #region Unity Functions => Awake, OnEnable
        private void OnEnable()
        {
            StartCoroutine(AddDefaultNextFrame());
        }

        private IEnumerator AddDefaultNextFrame()
        {
            yield return null;
            IInputManager iim = ManagerProvider.GetManager("InputManager") as IInputManager;
            iim.SetDefaultReceiver(this);
        }

        private void Awake()
        {
            this.GetComponent<Collider>().enabled = false;
        }
        #endregion
    }
}

