using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Data;

using System.IO;
using System.Collections.ObjectModel;

namespace TacticalBounce.Managers
{
    /*
     * Dependency Notes: LevelData, To strore levels
     */
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        #region Class Variables
        [SerializeField] private List<LevelDataStr> LevelList = new List<LevelDataStr>();

        private LevelData currentLevel;
        #endregion

        #region ILevelManager
        public string ManagerType { get { return "LevelManager"; } }

        public int LevelCount { get { return LevelList.Count; } }

        public bool LoadLevel(int index)
        {
            //if List null or 'out of index', return
            if (LevelList == null || LevelList.Count - 1 < index)
                return false;

            if(currentLevel != null)
            {
                currentLevel.ClearLevel();
            }

            LevelDataStr levelDataStr = LevelList[index];
            currentLevel = new LevelData();
            currentLevel.SetLevelData(levelDataStr);
            currentLevel.SetLevelTransform(this.transform);
            currentLevel.GenerateLevel();

            return true;
        }

        [ContextMenu("Add Current Level to List")]
        public void SaveLevel()
        {
            LevelData levelData = new LevelData();
            levelData.SetLevelTransform(this.transform);
            levelData.CollectLevelData();

            currentLevel = levelData;

            LevelList.Add(new LevelDataStr(levelData.GetLevelData().data));
            return;
        }
        #endregion

        #region Inspector Functions
        [ContextMenu("Load Last")]
        private void LoadLast()
        {
            int last = LevelList.Count - 1;

            //currentLevel = LevelList[last];

            LoadLevel(last);
        }

        [ContextMenu("Clear Level")]
        private void ClearLevel()
        {
            currentLevel.ClearLevel();
        }
        #endregion

        #region Unity Functions => Awake, OnDestroy
        private void Awake()
        {
            ManagerProvider.AddManager(this);
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager(this);
        }
        #endregion
    }

}
