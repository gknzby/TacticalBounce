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
                case GameAction.Stop:
                    break;
                case GameAction.Shot:
                    Shot();
                    break;
                case GameAction.Lost:
                    break;
                case GameAction.Win:
                    break;
                case GameAction.Restart:
                    RestartGame();
                    break;
                case GameAction.LoadLevel:
                    this.LoadLevel();
                    break;
                default:
                    break;
            }
        }

        private void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        #endregion

        #region Class Functions
        private void Shot()
        {
            OnGameStateChange?.Invoke(GameState.Shotted);
        }
        
        private void LoadLevel()
        {
            OnGameStateChange?.Invoke(GameState.Preparation);

            int level = PlayerPrefs.GetInt("Level");

            ILevelManager ilm = ManagerProvider.GetManager("LevelManager") as ILevelManager;
            ilm.LoadLevel(level);
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
            //Works after all awake and start functions
            StartCoroutine(AfterLoad());    
        }
        private IEnumerator AfterLoad()
        {
            yield return null; //Waiting first update functions
            IUIManager uiMng = ManagerProvider.GetManager("UIManager") as IUIManager;
            uiMng.ShowMenu("MainMenu");
        }


        private void OnDestroy()
        {
            ManagerProvider.RemoveManager(this);
        }
        #endregion


    }
}
