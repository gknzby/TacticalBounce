using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TacticalBounce.Components
{
    public class BallPath : MonoBehaviour
    {
        
        [SerializeField] private Material CorrectBallPath;
        [SerializeField] private Material WrongBallPath;
        
        private bool firstCall = true;
        private Vector3 defaultScale;

        //Always same width, independent of parents's scale
        private void ScaleSetting()
        {
            defaultScale = transform.localScale;
            defaultScale.x /= transform.lossyScale.x;
            defaultScale.y /= transform.lossyScale.y;
            defaultScale.z /= transform.lossyScale.z;
        }

        /*
         * Place itself between A and B point (or Start and End)
         * and adjust its scale according to distance
         * 
         * A..........\..........B 
         * First it puts itself in the middle
         * 
         * A..........|..........B
         * Then, looks at B point (In this way, it is perpendicular to the line between A and B
         * 
         * A.........___.........B
         * Rotate itself 90 degree
         * 
         * A_____________________B
         * Change vertical scale
         * 
         */
        public void DrawPath(Vector3 startPoint, Vector3 endPoint, bool isCorrectPath)
        {
            //Almost floor level height 
            startPoint.y = 0.1f;
            endPoint.y = 0.1f;

            if(firstCall)
            {
                ScaleSetting();
                firstCall = false;
            }

            //Mid point
            Vector3 midPoint = (startPoint + endPoint) / 2f;
            this.transform.position = midPoint;

            //Parallel to A to B direction
            this.transform.LookAt(endPoint);
            this.transform.Rotate(Vector3.right, 90f);

            //Gets distance between A and B point
            float pointDistance = Vector3.Distance(startPoint, endPoint);
            pointDistance = pointDistance < 1 ? 1 : pointDistance;
            Vector3 pathScale = defaultScale;
            pathScale.y *= pointDistance;
            this.transform.localScale = pathScale;

            //Material change
            this.GetComponent<MeshRenderer>().material = isCorrectPath ? CorrectBallPath : WrongBallPath;
        }
    }
}

