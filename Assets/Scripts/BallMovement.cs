using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TacticalBounce.Components
{
    public class BallMovement : MonoBehaviour
    {
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Managers.GameManager.Instance.OnGameStateChange += this.HandleGameStateChange;
        }

        private void InitialShot()
        {
            rb.velocity = this.transform.forward * 5f;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.CompareTag("Dummy"))
            {
                Vector3 target = other.transform.GetComponent<Dummy>().GetTarget();
                rb.velocity = target - this.transform.position;
            }
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

