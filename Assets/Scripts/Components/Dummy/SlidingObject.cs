using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TacticalBounce.Components
{
    public class SlidingObject : MonoBehaviour, IInputReceiver
    {
        #region Inspector Variables
        [SerializeField] private Transform StartPoint_T;
        [SerializeField] private Transform EndPoint_T;

        [Range(0f, 1f)]
        [SerializeField] private float SlideSensivity;
        #endregion

        #region Class Variables
        private Vector3 startPoint;
        private Vector3 endPoint;
        private Vector2 slideDirection;
        private float slidePosition = -1;
        #endregion

        #region IInputReceiver
        public void Cancel()
        {
            return;
        }

        public void Click()
        {
            SetDefaults();
            return;
        }

        public void Drag(Vector2 dragVec)
        {
            Slide(dragVec);
            return;
        }

        public void Release()
        {
            return;
        }
        #endregion

        #region Unity Functions
        private void Awake()
        {
            Debug.Assert(StartPoint_T != null, "Add start point to sliding object");
            Debug.Assert(EndPoint_T != null, "Add end point to sliding object");

            SetDefaults();
        }

        #endregion

        #region Class Functions
        private void SetDefaults()
        {
            startPoint = StartPoint_T.position;
            endPoint = EndPoint_T.position;

            Vector3 tempSlideDir = (endPoint - startPoint).normalized;
            slideDirection = new Vector2(tempSlideDir.x, tempSlideDir.z);
            slidePosition = slidePosition < 0 ? 0 : slidePosition;

            this.transform.position = startPoint;
        }

        private void Slide(Vector2 dragVector)
        {
            float slideValue = Vector2.Dot(dragVector, slideDirection) * this.SlideSensivity;

            slidePosition = Mathf.Clamp01(slidePosition + slideValue);
            
            this.transform.position = Vector3.Lerp(startPoint, endPoint, slidePosition);
        }

        #endregion
    }
}
