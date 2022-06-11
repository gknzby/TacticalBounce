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
        private void HandleGameStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    break;
                case GameState.Preparation:
                    break;
                case GameState.Shotted:
                    InitialShot();
                    break;
                case GameState.Goal:
                    break;
                case GameState.Loose:
                    break;
                default:
                    break;
            }
        }

        private void InitialShot()
        {
            rb.velocity = this.transform.forward * 5f;
        }


        private void OnTriggerEnter(Collider other)
        {
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

            Debug.Log("Goal");
        }

        private void GameLost()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            Debug.Log("Lost");
        }

        private IEnumerator FollowBall(Vector3 target)
        {
            yield return new WaitForFixedUpdate();

            Vector3 firstPos = this.transform.position;
            float timer = 0;

            while (timer < BallReachTime)
            {
                yield return new WaitForFixedUpdate();
                timer += Time.fixedDeltaTime;

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
            igm.OnGameStateChange += HandleGameStateChange;
        }
        #endregion
    }
}

