using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BattleCommander.Logic.Managers
{

    public enum EGameState
    {
        Menu,
        BattlePreparation,
        Battle,
        PostBattle
    }

    public class GameManager : MonoBehaviour
    {
        private GameManager() { }
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                return instance;
            }
            set
            {
                instance = value;
            }
        }

        private static GameManager instance;

        public EGameState GameState { get => _gameState; }
        private EGameState _gameState;

        public static event Action<EGameState> OnGameStateChanged;

        void Awake()
        {
            if (instance != null && instance != this)
                Destroy(gameObject);

            instance = this;
            _gameState = EGameState.Menu;
        }

        public void SetGameState(EGameState _state)
        {
            _gameState = _state;
            OnGameStateChanged?.Invoke(_state);
        }
    }
}
