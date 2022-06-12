using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Managers;
using TacticalBounce.Components;

namespace TacticalBounce.Managers
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        #region IInputManager
        public string ManagerType { get; set; }

        public void SetDefaultReceiver(IInputReceiver inputReceiver)
        {
            defaultReceiver = inputReceiver;
        }
        #endregion

        #region Inspector Variables
        [Min(1f)]
        [SerializeField] private float MouseSensivitiy = 50f;

        [Range(0f, 1f)]
        [SerializeField] private float ScreenDeadZone = 0.2f;
        #endregion

        #region Class Variables
        private IInputReceiver defaultReceiver;
        private IInputReceiver activeReceiver;

        private Vector2 prevPos;
        private Vector2 curPos;

        private bool isInputActive;
        #endregion

        #region Class Functions
        private IInputReceiver GetReceiverFromScreen(out Vector2 mousePos)
        {
            IInputReceiver newReceiver;
            mousePos = Input.mousePosition;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            Debug.Assert(defaultReceiver != null, "There is no default receiver in the scene");
            newReceiver = defaultReceiver;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<IInputReceiver>() != null)
                {
                    newReceiver = hit.transform.GetComponent<IInputReceiver>();
                }
                else if (hit.transform.parent.GetComponent<IInputReceiver>() != null)
                {
                    newReceiver = hit.transform.parent.GetComponent<IInputReceiver>();
                }
            }

            newReceiver.Click();

            return newReceiver;
        }

        private void ReleaseReceiver(IInputReceiver receiver)
        {
            receiver?.Release();
        }

        private void SendUpdateInfoToReceiver(IInputReceiver receiver, Vector2 oldPos, out Vector2 newPos)
        {
            newPos = Input.mousePosition;

            if (IsDeadZone(newPos))
            {
                isInputActive = false;
                if (waitForReleeaseCoroutine != null)
                    StopCoroutine(waitForReleeaseCoroutine);

                waitForReleeaseCoroutine = StartCoroutine(WaitForRelease());

                receiver.Cancel();
                return;
            }

            receiver.Drag(GetScaledDragValue(newPos, oldPos));
        }

        private Vector2 GetScaledDragValue(Vector2 aPoint, Vector2 bPoint)
        {
            Vector2 dragVec = aPoint - bPoint;
            dragVec /= (Screen.width);
            dragVec *= this.MouseSensivitiy;

            return dragVec;
        }

        private bool IsDeadZone(Vector2 mousePos)
        {
            float height = mousePos.y / Screen.height;
            if (height < this.ScreenDeadZone)
            {
                return true;
            }
            return false;
        }

        private void HandleGameStateChange(Managers.GameState newState)
        {
            switch (newState)
            {
                case Managers.GameState.Preparation:
                    isInputActive = false;
                    if (waitForReleeaseCoroutine == null)
                        waitForReleeaseCoroutine = StartCoroutine(WaitForRelease());
                    break;
                default:
                    isInputActive = false;
                    break;
            }
        }

        private Coroutine waitForReleeaseCoroutine;

        private IEnumerator WaitForRelease()
        {
            while (Input.GetMouseButton(0))
            {
                yield return null;
            }
            yield return null;
            isInputActive = true;
        }
        #endregion

        #region Unity Functions
        private void Awake()
        {
            isInputActive = false;

            this.ManagerType = "InputManager";
            ManagerProvider.AddManager(this);
        }

        private void Start()
        {
            IGameManager igm = ManagerProvider.GetManager("GameManager") as IGameManager;
            igm.OnGameStateChange += this.HandleGameStateChange;
        }

        private void Update()
        {
            if (!isInputActive) return;

            if (Input.GetMouseButtonDown(0))
            {
                activeReceiver = GetReceiverFromScreen(out curPos);
                prevPos = curPos;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ReleaseReceiver(activeReceiver);
            }
            else if (Input.GetMouseButton(0))
            {
                SendUpdateInfoToReceiver(activeReceiver, prevPos, out curPos);
                prevPos = curPos;
            }
        }

        private void OnApplicationQuit()
        {
            ManagerProvider.RemoveManager(this);
        }
        #endregion
    }
}

