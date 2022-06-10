using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TacticalBounce.Components
{
    public class BallPath : MonoBehaviour
    {
        private bool firstCall = true;
        private Vector3 defaultScale;

        private void ScaleSetting()
        {
            defaultScale = transform.localScale;
            defaultScale.x /= transform.lossyScale.x;
            defaultScale.y /= transform.lossyScale.y;
            defaultScale.z /= transform.lossyScale.z;
        }

        public void DrawPath(Vector3 startPoint, Vector3 endPoint)
        {
            startPoint.y = 0.1f;
            endPoint.y = 0.1f;

            if(firstCall)
            {
                ScaleSetting();
                firstCall = false;
            }

            Vector3 midPoint = (startPoint + endPoint) / 2f;
            this.transform.position = midPoint;

            this.transform.LookAt(endPoint);
            this.transform.Rotate(Vector3.right, 90f);

            float pointDistance = Vector3.Distance(startPoint, endPoint);
            pointDistance = pointDistance < 1 ? 1 : pointDistance;
            Vector3 pathScale = defaultScale;
            pathScale.y *= pointDistance;
            this.transform.localScale = pathScale;
        }
    }
}

