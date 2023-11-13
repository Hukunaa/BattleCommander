using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

[RequireComponent(typeof(UnitController))]
[RequireComponent(typeof(UnitDisplay))]
[RequireComponent(typeof(UnitAnimator))]
public class Unit : MonoBehaviour
{
    private UnitController _controller;
    private UnitSettings _settings;
    private UnitDisplay _display;
    private UnitAnimator _animator;
    public DamageNumber _dmgPopup;
    public DamageNumber _healPopup;

    private int _health;
    private bool _isPlayerSide;
    private bool _isFighting;
    private bool _alive;

    [SerializeField]
    private float _maxEnemyDist;
    private Unit _currentTarget;
    private float _attackTimer = 0.0f;

	void Awake()
	{
        _controller = GetComponent<UnitController>();
        _animator = GetComponent<UnitAnimator>();
        _display = GetComponent<UnitDisplay>();
        BattleManager.OnBattleStart += Fight;
        BattleManager.OnBattleEnd += Stop;
    }
	void Start()
    {
        _controller = GetComponent<UnitController>();
        _display = GetComponent<UnitDisplay>();
        _currentTarget = null;
    }

    public void GenerateUnit()
    {
        _settings = new UnitSettings((UnitSettings.ShapeType)(UnityEngine.Random.Range(0, 100) % Enum.GetNames(typeof(UnitSettings.ShapeType)).Length),
                                     (UnitSettings.SizeType)(UnityEngine.Random.Range(0, 100) % Enum.GetNames(typeof(UnitSettings.SizeType)).Length),
                                     (UnitSettings.ColorType)(UnityEngine.Random.Range(0, 100) % Enum.GetNames(typeof(UnitSettings.ColorType)).Length));
        _health = _settings.Health;
        _display.UpdateUnitDisplay(_settings, _isPlayerSide);
    }

    public void Fight()
    {
        if(_alive)
		{
            ChooseEnemy();
            _controller.ActivateAgent();
            _isFighting = true;
		}
    }

    public void Stop()
    {
        _controller.DisableAgent();
        _isFighting = false;
    }

    private void ChooseEnemy()
    {
        _currentTarget = _controller.SelectTarget(IsPlayerSide);
        if (_currentTarget == null || _currentTarget.gameObject.activeSelf == false)
            Stop();
    }

    public void ApplyDamage()
    {
        bool enemyAlive = _currentTarget.ReceiveDamage(Settings.Attack);
        _animator.Play();

        if (!enemyAlive)
            ChooseEnemy();
    }

    public void Heal(int _heal)
    {
        if (_alive)
        {
            _health += _heal;

            if (_health > Settings.Health)
                _health = Settings.Health;

            _healPopup.Spawn(transform.position + Vector3.up, _heal);
        }
    }
    public bool ReceiveDamage(int _dmg)
    {
        if (_alive)
        {
            _health -= _dmg;
            _dmgPopup.Spawn(transform.position + Vector3.up, _dmg);
            _display.ReceiveHit();
        }
        if (_health <= 0)
        {
            Die();
            return false;
        }
		return true;
	}

	private void Die()
    {
        _alive = false;
        Stop();
        gameObject.SetActive(false);
    }

	private void OnDestroy()
	{
        BattleManager.OnBattleStart -= Fight;
        BattleManager.OnBattleEnd -= Stop;
    }

	void Update()
    {
        if(IsFighting)
        {
            if (_currentTarget.gameObject.activeSelf == false)
                ChooseEnemy();

            if (_currentTarget != null)
            {
                if (Time.time > _attackTimer)
                {
                    if (Vector3.Distance(transform.position, _currentTarget.transform.position) < Settings.AttackRange + 0.1f)
                    {
                        ApplyDamage();
                    }
                    _attackTimer = Time.time + _settings.AttackSpeed;
                }
            }
        }
    }

    public UnitSettings Settings { get => _settings;}
    public int Health { get => _health; }
    public bool IsPlayerSide { get => _isPlayerSide; set => _isPlayerSide = value; }
    public bool IsFighting { get => _isFighting; }
    public float MaxEnemyDist { get => _maxEnemyDist; }
	public bool Alive { get => _alive; set => _alive = value; }
}
