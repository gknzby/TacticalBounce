using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace TacticalBounce.Managers
{
    public enum GameState
    {
        Menu,
        Preparation,
        Shotted,
        Goal,
        Loose
    }

    public class GameManager : MonoBehaviour, IGameManager
    {
        #region IGameManager
        public string ManagerType { get { return "GameManager"; } }
        public void SendGameAction(GameAction gameAction)
        {
            switch (gameAction)
            {
                case GameAction.PauseGame:
                    StopGame();
                    break;
                case GameAction.Shot:
                    Shot();
                    break;
                case GameAction.Lost:
                    GameLost();
                    break;
                case GameAction.Win:
                    GameWin();
                    break;
                case GameAction.Restart:
                    RestartGame();
                    break;
                case GameAction.LoadLevel:
                    LoadLevel();
                    break;
                case GameAction.EndGame:
                    EndGame();
                    break;
                case GameAction.ResumeGame:
                    ResumeGame();
                    break;
                default:
                    break;
            }
        }

        IUIManager uiManager;
        IInputManager inputManager;

        private void EndGame()
        {
            StopGame();
            uiManager.ShowMenu("EndGameMenu");
            PlayerPrefs.SetInt("Level", 0);
        }

        private void GameWin()
        {
            StopGame();
            uiManager.ShowMenu("WinMenu");
        }

        private void GameLost()
        {
            StopGame();
            uiManager.ShowMenu("LostMenu");
        }

        private void ResumeGame()
        {
            Time.timeScale = 1f;
            uiManager.ShowMenu("HUD");
            inputManager.StartSendingInputs();
        }

        private void StopGame()
        {
            Time.timeScale = 0f;
            uiManager.HideMenu("HUD");
            inputManager.StopSendingInputs();
        }

        private void RestartGame()
        {
            uiManager.ShowMenu("MainMenu");
        }
        #endregion

        #region Class Functions
        private void Shot()
        {
            inputManager.StopSendingInputs();
        }
        
        private void LoadLevel()
        {
            PlayerPrefs.Save();

            int level = PlayerPrefs.GetInt("Level");

            ILevelManager ilm = ManagerProvider.GetManager("LevelManager") as ILevelManager;
            if(ilm.LoadLevel(level))
            {
                ResumeGame();
            }
            else
            {
                SendGameAction(GameAction.EndGame);
            }
        }
        #endregion

        #region Unity Functions => Awake, Start, OnDestroy
        private void Awake()
        {
            ManagerProvider.AddManager(this);
        }

        private void Start()
        {
            uiManager = ManagerProvider.GetManager("UIManager") as IUIManager;
            inputManager = ManagerProvider.GetManager("InputManager") as IInputManager;

            //Works after all awake functions and start functions
            StartCoroutine(AfterLoad());    
        }
        private IEnumerator AfterLoad()
        {
            yield return null; //Waiting first update functions
            uiManager.ShowMenu("MainMenu");
        }


        private void OnDestroy()
        {
            ManagerProvider.RemoveManager(this);
        }
        #endregion

    }
}
