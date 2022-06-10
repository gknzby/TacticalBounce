using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TacticalBounce.Components
{
    public class BallMovement : MonoBehaviour
    {
        private Rigidbody rb;
        private Vector3 targetPos;
        private bool followTarget = false;

        private float lastTouchTime;
        private float timeOutLimit = 2f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Managers.GameManager.Instance.OnGameStateChange += this.HandleGameStateChange;
        }

        //private void FixedUpdate()
        //{
        //    if (!followTarget) return;

        //    if(timeOutLimit < Time.time - lastTouchTime)
        //    {
        //        Debug.Log("Lost");
        //        followTarget = false;
        //        return;
        //    }


        //}

        private void InitialShot()
        {
            rb.velocity = this.transform.forward * 5f;
        }


        private Coroutine activeCoroutine;
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

        [Min(0.5f)]
        [SerializeField] private float BallReachTime = 1f;
        private IEnumerator FollowBall(Vector3 target)
        {
            yield return new WaitForFixedUpdate();

            Vector3 firstPos = this.transform.position;
            float timer = 0;

            while (0.51f < Vector3.Distance(target, this.transform.position))
            {
                yield return new WaitForFixedUpdate();
                timer += Time.fixedDeltaTime;

                rb.MovePosition(Vector3.Lerp(firstPos, target, timer/BallReachTime));
            }

            yield return new WaitForSeconds(0.1f);
            GameLost();
        }

        private void HandleGameStateChange(Managers.GameState newState)
        {
            switch (newState)
            {
                case Managers.GameState.Shotted:
                    InitialShot();
                    break;
                default:
                    break;
            }
        }

        
    }
}

