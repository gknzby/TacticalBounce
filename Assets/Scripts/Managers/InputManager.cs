using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Components
{
    public class InputManager : MonoBehaviour
    {
        public GameObject DefRec_Obj; //TEMP
        private IInputReceiver defaultReceiver;
        private IInputReceiver activeReceiver;

        private Vector2 prevPos;
        private Vector2 curPos;

        private bool isInputActive;

        private void Awake()
        {
            isInputActive = false;
            defaultReceiver = DefRec_Obj.GetComponent<IInputReceiver>();
        }

        private void Start()
        {
            Managers.GameManager.Instance.OnGameStateChange += this.HandleGameStateChange;
        }

        private void Update()
        {
            if (!isInputActive) return;

            if (Input.GetMouseButtonDown(0))
            {
                curPos = Input.mousePosition;

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(curPos);
                if (Physics.Raycast(ray, out hit) && hit.transform.GetComponent<IInputReceiver>() != null)
                {
                    activeReceiver = hit.transform.GetComponent<IInputReceiver>();
                }
                else
                {
                    activeReceiver = defaultReceiver;
                }

                activeReceiver.Click();
                prevPos = curPos;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                activeReceiver.Release();
            }
            else if (Input.GetMouseButton(0))
            {
                curPos = Input.mousePosition;
                Vector2 dragVec = curPos - prevPos;
                activeReceiver.Drag(dragVec);
                prevPos = curPos;
            }
        }

        private void HandleGameStateChange(Managers.GameState newState)
        {
            switch (newState)
            {
                case Managers.GameState.Preparation:
                    StartCoroutine(WaitForRelease());
                    break;
                default:
                    isInputActive = false;
                    break;
            }
        }

        private IEnumerator WaitForRelease()
        {
            while(Input.GetMouseButton(0))
            {
                yield return null;
            }
            yield return null;
            isInputActive = true;
        }
    }
}

