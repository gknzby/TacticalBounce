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

    public class GameManager : Helpers.SingletonMono<GameManager>
    {
        public event Action<GameState> OnGameStateChange;

        public void StartGame()
        {
            OnGameStateChange?.Invoke(GameState.Preparation);
        }

        public void Shotted()
        {
            OnGameStateChange?.Invoke(GameState.Shotted);
        }
    }
}
