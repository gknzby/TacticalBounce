using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticalBounce.Managers;

namespace TacticalBounce.Components
{
    public class UILevelsMenu : UIMenu
    {
        [SerializeField] private GameObject LevelBtnPrefab;
        [SerializeField] private Transform GridTransform;

        private List<GameObject> generatedLevelButtons;

        public override void ShowMenu()
        {
            base.ShowMenu();

            GenerateLevelButtons();
        }

        public override void HideMenu()
        {
            base.HideMenu();

            ClearLevelButtons();
        }

        private void ClearLevelButtons()
        {
            if (generatedLevelButtons == null)
                return;

            foreach (GameObject levelButton in generatedLevelButtons)
            {
                Destroy(levelButton);
            }

            generatedLevelButtons.Clear();
        }

        private void GenerateLevelButtons()
        {
            ILevelManager levelManager = ManagerProvider.GetManager("LevelManager") as ILevelManager;
            int levelCount = levelManager.LevelCount;

            generatedLevelButtons = new List<GameObject>();

            for (int i = 0; i < levelCount; i++)
            {
                GameObject levelBtn = Instantiate(LevelBtnPrefab, GridTransform.transform);
                levelBtn.GetComponent<UIComponent>().value = i.ToString();
                levelBtn.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
                levelBtn.name = (i + 1).ToString();

                generatedLevelButtons.Add(levelBtn);
            }
        }
    }
}
