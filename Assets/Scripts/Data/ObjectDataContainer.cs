using System.Collections;
using System.Collections.Generic;
using TacticalBounce.Managers;
using UnityEngine;


namespace TacticalBounce.Data
{
    public abstract class ObjectDataContainer : MonoBehaviour, IObjectDataContainer
    {
        #region IObjectDataContainer
        public virtual IObjectData DataObject { get; set; }

        public virtual void CreateObject()
        {
            DataObject.CreateObject(this.transform);
        }

        public virtual void CollectData()
        {
            DataObject.CollectData();
        }

        public virtual List<string> GetObjectData()
        {
            return DataObject.GetObjectData();
        }

        public virtual void SetObjectData(List<string> dataString)
        {
            DataObject.SetObjectData(dataString);
        }
        #endregion
    }
}
