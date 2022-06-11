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
        public string ManagerType { get; set; }
        public event Action<GameState> OnGameStateChange;
        public void SendGameAction(GameAction gameAction)
        {
            switch (gameAction)
            {
                case GameAction.MainMenu:
                    break;
                case GameAction.StartGame:
                    StartGame();
                    break;
                case GameAction.Stop:
                    break;
                case GameAction.Shot:
                    Shot();
                    break;
                case GameAction.Lost:
                    break;
                case GameAction.Win:
                    break;
                case GameAction.Retry:
                    break;
                case GameAction.Restart:
                    break;
                case GameAction.Next:
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Class Functions
        private void Shot()
        {
            OnGameStateChange?.Invoke(GameState.Shotted);
        }

        private void StartGame()
        {
            OnGameStateChange?.Invoke(GameState.Preparation);
        }

        

        private IEnumerator AfterLoad()
        {
            yield return null; //Waiting first update functions
            IUIManager uiMng = ManagerProvider.GetManager("UIManager") as IUIManager;
            uiMng.ShowMenu("MainMenu");
        }
        #endregion

        #region Unity Functions
        private void Awake()
        {
            ManagerType = "GameManager";
            ManagerProvider.AddManager(this);
        }

        private void Start()
        {
            StartCoroutine(AfterLoad());    
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager(this);
        }
        #endregion


    }
}
