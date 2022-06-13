using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Managers;
using System.Collections.ObjectModel;

namespace TacticalBounce.Data
{
    [System.Serializable]
    public class ObjectDataStr
    {
        public List<string> data = new List<string>();

        public ObjectDataStr(List<string> data = null)
        {
            if(data == null)
            {
                this.data = new List<string>();
            }
            else
            {
                this.data = new List<string>(data);
            }
        }
    }

    [System.Serializable]
    public class LevelDataStr
    {
        public List<ObjectDataStr> data = new List<ObjectDataStr>();

        public LevelDataStr(List<ObjectDataStr> data = null)
        {
            if(data == null)
            {
                this.data = new List<ObjectDataStr>();
            }
            else
            {
                this.data = new List<ObjectDataStr>(data);
            }
        }
    }

    /*
     * Dependency Note: IObjectDataContainer, To collect data
     */
    public class LevelData
    {
        #region Class Variables
        private LevelDataStr levelDataStr;
        private List<GameObject> createdObjects;
        private Transform levelTransform;
        #endregion

        #region Class Functions
        public void CollectLevelData()
        {
            Debug.Assert(levelTransform != null, "First, set level transform to LevelData");

            if(levelDataStr != null)
            {
                levelDataStr.data.Clear();
            }
            else
            {
                levelDataStr = new LevelDataStr();
            }


            for(int i = 0; i < levelTransform.childCount; i++)
            {
                Debug.Assert(levelTransform.GetChild(i).GetComponent<IObjectDataContainer>() != null, "All childeren objects of Level must have DataContainer!");

                //First get data container
                IObjectDataContainer iodc = levelTransform.GetChild(i).GetComponent<IObjectDataContainer>();

                //Collect object data
                iodc.CollectData();

                //Get String Version
                ObjectDataStr objectString = new ObjectDataStr(iodc.GetObjectData());

                //Add to list
                levelDataStr.data.Add(objectString);
            }
        }

        public void GenerateLevel()
        {
            if(levelDataStr == null)
            {
                return;
            }

            ClearLevel();
            createdObjects = new List<GameObject>();

            foreach(ObjectDataStr objectString in levelDataStr.data)
            {
                PoolPrefabType pot = (PoolPrefabType)System.Enum.Parse(typeof(PoolPrefabType), objectString.data[0]);
                GameObject newObj;

#if UNITY_EDITOR
                GameObject prefab = PoolPrefabs.GetPoolPrefab(pot);
                if (Application.isPlaying) //if it is in play mode, use ObjectPool
                {
                    IObjectPool iop = ManagerProvider.GetManager("ObjectPool") as IObjectPool;
                    newObj = iop.GetPoolObject(pot);
                }
                else //In edit mode, managers don't exist. 
                {
                    newObj = Object.Instantiate(prefab, levelTransform);
                }
#else //Built version doesn't need to check play/edit 
                IObjectPool iop = ManagerProvider.GetManager("ObjectPool") as IObjectPool;
                newObj = iop.GetPoolObject(pot);
#endif

                IObjectDataContainer iodc = newObj.GetComponent<IObjectDataContainer>();

                iodc.SetObjectData(new List<string>(objectString.data));
                iodc.CreateObject();

                createdObjects.Add(newObj);
            }
        }

        public LevelDataStr GetLevelData()
        {
            return levelDataStr;
        }

        public void SetLevelData(LevelDataStr levelString)
        {
            this.levelDataStr = levelString;
        }

        public void SetLevelTransform(Transform levelTransform)
        {
            this.levelTransform = levelTransform;
        }

        public void ClearLevel()
        {
            if (createdObjects == null)
            {
                return;
            }

            foreach (GameObject go in createdObjects)
            {
#if UNITY_EDITOR
                if(Application.isPlaying) //if it is in play mode, use ObjectPool
                {
                    IObjectPool iop = ManagerProvider.GetManager("ObjectPool") as IObjectPool;
                    iop.ReturnPoolObject(go);
                }
                else //In edit mode, managers don't exist. 
                {
                    Object.DestroyImmediate(go);
                }
#else //Built version doesn't need to check play/edit
                IObjectPool iop = ManagerProvider.GetManager("ObjectPool") as IObjectPool;
                iop.ReturnPoolObject(go);
#endif
            }

            createdObjects.Clear();
        }
#endregion
    }
}
