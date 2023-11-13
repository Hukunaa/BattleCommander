using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BattleManager : MonoBehaviour
{
    private BattleManager() { }
    public static BattleManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(BattleManager)) as BattleManager;

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static BattleManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }

    public List<Unit> _playerArmy;
    public List<Unit> _enemyArmy;
    private List<Unit> _unitPool;
    public GameObject UnitPrefab;
    public GridManager PlayerGrid;
    public GridManager EnemyGrid;

    public static event Action OnBattleStart;
    public static event Action OnBattleEnd;
    public static event Action OnBattleVictory;
    public static event Action OnBattleLoose;

    public int maxPoolSize;
    private bool _lastBattleResult;

    private void Start()
    {
        _playerArmy = new List<Unit>();
        _enemyArmy = new List<Unit>();
        _unitPool = new List<Unit>();

        for(int i = 0; i < maxPoolSize * 2; ++i)
		{
            Unit unit = Instantiate(UnitPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Unit>();
            unit.gameObject.name = "Unit " + i;
            unit.IsPlayerSide = true;
            unit.Alive = false;
            unit.gameObject.SetActive(false);
            _unitPool.Add(unit);
        }
        GameManager.OnGameStateChanged += GameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChanged;
    }

    private void Update()
    {
        if(GameManager.Instance.GameState == GameManager.GameStateType.Battle)
        {
            int playerCount = PlayerArmy.Where(c => c.Alive).Count();
            int enemyCount = EnemyArmy.Where(c => c.Alive).Count();
            //Triggers the end of a battle
            if (playerCount == 0 || enemyCount == 0)
            {
                if (playerCount == 0)
                    _lastBattleResult = false;
                else if (enemyCount == 0)
                    _lastBattleResult = true;

                GameManager.Instance.SetGameState(GameManager.GameStateType.PostBattle);
            }
        }
    }

    private void GameStateChanged(GameManager.GameStateType _state)
    {
        switch(_state)
        {
            case GameManager.GameStateType.BattlePreparation:
                GenerateArmies();
                break;

            case GameManager.GameStateType.Battle:
                StartBattle();
                break;

            case GameManager.GameStateType.PostBattle:
                EndBattle();
                break;
        }
    }
    private void GenerateArmies()
    {
        ResetArmies();

        for (int i = 0; i < PlayerGrid.SizeXY; ++i)
        {
            if (_unitPool.Count > 0)
            {
                Unit poolUnit = _unitPool[_unitPool.Count - 1];
                poolUnit.gameObject.name = "Player Unit " + i;
                poolUnit.transform.position = PlayerGrid.GetPosition(i);
                poolUnit.transform.rotation = PlayerGrid.transform.rotation;
                poolUnit.IsPlayerSide = true;
                poolUnit.Alive = true;
                poolUnit.gameObject.SetActive(true);
                poolUnit.GenerateUnit();
                _playerArmy.Add(poolUnit);
                _unitPool.Remove(poolUnit);
            }
        }

        for (int i = 0; i < EnemyGrid.SizeXY; ++i)
        {
            if(_unitPool.Count > 0)
			{
                Unit poolUnit = _unitPool[_unitPool.Count - 1];
                poolUnit.gameObject.name = "Enemy Unit " + i;
                poolUnit.transform.position = EnemyGrid.GetPosition(i);
                poolUnit.transform.rotation = EnemyGrid.transform.rotation;
                poolUnit.IsPlayerSide = false;
                poolUnit.Alive = true;
                poolUnit.gameObject.SetActive(true);
                poolUnit.GenerateUnit();
                _enemyArmy.Add(poolUnit);
                _unitPool.Remove(poolUnit);
			}
        }
    }

    public void RandomizeUnits()
	{
        foreach (Unit u in _playerArmy)
            u.GenerateUnit();

        foreach (Unit t in _enemyArmy)
            t.GenerateUnit();
    }
    private void ResetArmies()
    {
        _playerArmy.ForEach(c => { c.Alive = false; c.gameObject.SetActive(false); _unitPool.Add(c); });
        _playerArmy.Clear();
        _enemyArmy.ForEach(c => { c.Alive = false; c.gameObject.SetActive(false); _unitPool.Add(c); });
        _enemyArmy.Clear();
    }

    private void StartBattle()
    {
        OnBattleStart?.Invoke();
    }

    private void EndBattle()
    {
        OnBattleEnd?.Invoke();
        if (PlayerArmy.Count == 0)
            OnBattleLoose?.Invoke();
        else if (EnemyArmy.Count == 0)
            OnBattleVictory?.Invoke();
    }

    public List<Unit> PlayerArmy { get => _playerArmy; }
    public List<Unit> EnemyArmy { get => _enemyArmy; }
    public bool LastBattleResult { get => _lastBattleResult; }
}
