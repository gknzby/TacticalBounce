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

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public event Action<GameState> OnGameStateChange;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void StartGame()
        {
            OnGameStateChange?.Invoke(GameState.Preparation);
        }

        public void Shotted()
        {
            OnGameStateChange?.Invoke(GameState.Shotted);
        }

#if UNITY_EDITOR
        private void OnApplicationQuit() //Unity lifecycle, Edit mode is not 'There is no object' mode. Statics can be dangerous and dreadful.
        {
            Instance = null;
            Destroy(this.gameObject);
        }
#endif
    }
}
