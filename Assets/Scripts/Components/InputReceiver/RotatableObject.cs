using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Components
{
    public class RotatableObject : MonoBehaviour, IInputReceiver
    {
        #region IInputReceiver
        public void Cancel()
        {
            return;
        }

        public void Click()
        {
            RotateObject();
            return;
        }

        public void Drag(Vector2 dragVec)
        {
            return;
        }

        public void Release()
        {
            return;
        }
        #endregion

        #region Class Functions
        private void RotateObject()
        {
            this.transform.Rotate(this.transform.up, 45f);
        }
        #endregion
    }
}

