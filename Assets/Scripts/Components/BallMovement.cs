using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Managers;


namespace TacticalBounce.Components
{
    public class BallMovement : MonoBehaviour
    {
        #region Inspector Variables
        [Min(0.5f)]
        [SerializeField] private float BallReachTime = 1f;
        #endregion

        #region Class Variables
        private Rigidbody rb;
        private Coroutine activeCoroutine;
        #endregion

        #region Class Functions

        private void OnTriggerEnter(Collider other)
        {
            if(rb.constraints == RigidbodyConstraints.FreezeAll)
            {
                rb.constraints = RigidbodyConstraints.None;
            }

            if(other.transform.CompareTag("Dummy"))
            {
                if(activeCoroutine != null)
                {
                    StopCoroutine(activeCoroutine);
                }

                Vector3 targetPos = other.transform.GetComponent<Dummy>().GetTarget();

                activeCoroutine = StartCoroutine(FollowBall(targetPos));
            }
            else if(other.transform.CompareTag("Goal"))
            {
                if (activeCoroutine != null)
                {
                    StopCoroutine(activeCoroutine);
                }

                GameWin();
            }
        }

        private void GameWin()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            IGameManager igm = ManagerProvider.GetManager("GameManager") as IGameManager;
            igm.SendGameAction(GameAction.Win);
        }

        private void GameLost()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            IGameManager igm = ManagerProvider.GetManager("GameManager") as IGameManager;
            igm.SendGameAction(GameAction.Lost);
        }

        private IEnumerator FollowBall(Vector3 target)
        {
            yield return new WaitForFixedUpdate();

            Vector3 firstPos = this.transform.position;
            float timer = 0;
            Vector3 angulatVel = new Vector3((target.z - firstPos.z), 0, (target.x - firstPos.x)) * 20f;

            while (timer < BallReachTime)
            {
                yield return new WaitForFixedUpdate();
                timer += Time.fixedDeltaTime;

                rb.angularVelocity = angulatVel;
                rb.MovePosition(Vector3.Lerp(firstPos, target, timer/BallReachTime));
            }

            yield return new WaitForSeconds(0.1f);
            GameLost();
        }
        #endregion

        #region Unity Functions
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

        }

        private void Start()
        {
            IGameManager igm = ManagerProvider.GetManager("GameManager") as IGameManager;

            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        #endregion
    }
}

