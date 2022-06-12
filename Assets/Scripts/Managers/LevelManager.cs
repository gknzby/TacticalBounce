using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Data;


namespace TacticalBounce.Managers
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        #region Class Variables
        [System.Serializable]
        public struct LevelMini
        {
            public List<LevelDataString> levelDiri;
            public LevelMini(List<LevelDataString> levelo)
            {
                levelDiri = levelo;
            }
        }
        [SerializeField] private List<LevelMini> LevelList = new List<LevelMini>();

        private LevelData currentLevel;
        private int currentLevelIndex = -1;
        #endregion

        #region ILevelManager
        public string ManagerType { get; set; }


        public int LevelCount { get { return LevelList.Count; } }

        public void LoadLevel(int index)
        {
            //if List null or 'out of index', return
            if (LevelList == null || LevelList.Count - 1 < index)
                return;

            if(currentLevel != null)
            {
                currentLevel.ClearLevel();
            }

            List<LevelDataString> levelDataCollection = LevelList[index].levelDiri;
            currentLevel = new LevelData();
            currentLevel.SetLevelData(levelDataCollection);
            currentLevel.SetLevelTransform(this.transform);
            currentLevel.GenerateLevel();

            currentLevelIndex = index;
        }

        public void NextLevel()
        {
            LoadLevel(++currentLevelIndex);
        }

        public void RetryLevel()
        {
            LoadLevel(currentLevelIndex);
        }

        [ContextMenu("Add Current Level to List")]
        public void SaveLevel()
        {
            LevelData levelData = new LevelData();
            levelData.SetLevelTransform(this.transform);
            levelData.CollectLevelData();

            currentLevel = levelData;

            LevelList.Add(new LevelMini(levelData.GetLevelData()));
            return;
        }
        #endregion

        #region Class Functions
        [ContextMenu("Load Last")]
        public void LoadLast()
        {
            int last = LevelList.Count - 1;

            //currentLevel = LevelList[last];

            LoadLevel(last);
        }

        [ContextMenu("Clear Level")]
        public void ClearLevel()
        {
            currentLevel.ClearLevel();
        }
        #endregion

        #region Unity Functions
        private void Awake()
        {
            ManagerType = "LevelManager";
            ManagerProvider.AddManager(this);
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager(this);
        }
        #endregion
    }

}
