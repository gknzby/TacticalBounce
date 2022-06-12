using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Managers;

namespace TacticalBounce.Data
{
    [System.Serializable]
    public struct LevelDataString
    {
        public List<string> dataStringList;
        public LevelDataString(List<string> dataStringList)
        {
            this.dataStringList = dataStringList;
        }
    }

    public class LevelData
    {
        #region Class Variables
        private List<LevelDataString> levelString;
        private List<GameObject> createdObjects;
        private Transform levelTransform;
        #endregion

        #region Class Functions
        public void CollectLevelData()
        {
            Debug.Assert(levelTransform != null, "First, set level transform to LevelData");

            if(levelString != null)
            {
                levelString.Clear();
            }
            else
            {
                levelString = new List<LevelDataString>();
            }


            for(int i = 0; i < levelTransform.childCount; i++)
            {
                Debug.Assert(levelTransform.GetChild(i).GetComponent<IObjectDataContainer>() != null, "All childeren objects must have DataContainer!");

                //First get data container
                IObjectDataContainer iodc = levelTransform.GetChild(i).GetComponent<IObjectDataContainer>();

                //Collect object data
                iodc.CollectData();


                //Get String Version
                LevelDataString levelChild = new LevelDataString(iodc.GetObjectData());

                //Add to list
                levelString.Add(levelChild);
            }
        }

        public void GenerateLevel()
        {
            if(levelString == null)
            {
                return;
            }

            if(createdObjects == null)
            {
                createdObjects = new List<GameObject>();
            }
            else
            {
                createdObjects.Clear();
            }

            foreach(LevelDataString level in levelString)
            {
                PoolPrefabType pot = (PoolPrefabType)System.Enum.Parse(typeof(PoolPrefabType), level.dataStringList[0]);
                GameObject prefab = PoolPrefabs.GetPoolPrefab(pot);

                GameObject newObj;

                if (Application.isPlaying)
                {
                    IObjectPool iop = ManagerProvider.GetManager("ObjectPool") as IObjectPool;
                    newObj = iop.GetPoolObject(prefab);
                }
                else
                {
                    newObj = Object.Instantiate(prefab, levelTransform);
                }
                IObjectDataContainer iodc = newObj.GetComponent<IObjectDataContainer>();

                iodc.SetObjectData(level.dataStringList);
                iodc.CreateObject();

                createdObjects.Add(newObj);
            }
        }

        public List<LevelDataString> GetLevelData()
        {
            return levelString;
        }

        public void SetLevelData(List<LevelDataString> levelString)
        {
            this.levelString = levelString;
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
                
                if(Application.isPlaying)
                {
                    IObjectPool iop = ManagerProvider.GetManager("ObjectPool") as IObjectPool;
                    iop.ReturnPoolObject(go);
                }
                else
                {
                    Object.DestroyImmediate(go);
                }
            }

            createdObjects.Clear();
        }
        #endregion
    }
}
