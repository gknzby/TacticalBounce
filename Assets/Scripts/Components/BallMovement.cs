using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Managers;


namespace TacticalBounce.Components
{
    /*
     * Dependency Notes :
     * GameManager => to send GameAction.Win and GameAction.Lost
     * Dummy => to get target position
     */
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

        //Follows target in a straight line, can be changeable later eaisly (e.g with Bezier Curve or an animation)
        private IEnumerator FollowBall(Vector3 target)
        {
            yield return new WaitForFixedUpdate();

            Vector3 firstPos = this.transform.position;
            float timer = 0;

            //Try to add spin to ball
            Vector3 angulatVel = new Vector3((target.z - firstPos.z), 0, (target.x - firstPos.x)) * 20f;

            while (timer < BallReachTime)
            {
                yield return new WaitForFixedUpdate();
                timer += Time.fixedDeltaTime;

                rb.angularVelocity = angulatVel;
                rb.MovePosition(Vector3.Lerp(firstPos, target, timer/BallReachTime));
            }

            //Ball must trigger any collider in reach time. If not, it means lost
            yield return new WaitForSeconds(0.1f);
            GameLost();
        }
        #endregion

        #region Unity Functions => Awake, Start, OnTriggerEnter
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        //Ball cannot change its position until triggered by other objects
        private void OnTriggerEnter(Collider other)
        {
            if (rb.constraints == RigidbodyConstraints.FreezeAll)
            {
                rb.constraints = RigidbodyConstraints.None;
            }

            if (other.transform.CompareTag("Dummy"))
            {
                if (activeCoroutine != null)
                {
                    StopCoroutine(activeCoroutine);
                }

                Vector3 targetPos = other.transform.GetComponent<Dummy>().GetTarget();

                activeCoroutine = StartCoroutine(FollowBall(targetPos));
            }
            else if (other.transform.CompareTag("Goal"))
            {
                if (activeCoroutine != null)
                {
                    StopCoroutine(activeCoroutine);
                }

                GameWin();
            }
        }
        #endregion
    }
}

