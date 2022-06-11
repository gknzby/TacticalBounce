using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalBounce.Data
{
    public interface IObjectDataContainer
    {
        IObjectData DataObject { get; set; }
    }
}
