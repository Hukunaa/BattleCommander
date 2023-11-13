using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private UnitController _target;
    private List<Unit> _targetList;
    private Unit _selfUnit;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _selfUnit = GetComponent<Unit>();
    }

    public void ActivateAgent()
    {
        _agent.stoppingDistance = GetComponent<Unit>().Settings.AttackRange;
        _agent.speed = _selfUnit.Settings.Speed;
        if (_agent != null)
            _agent.enabled = true;
    }

    public void DisableAgent()
    {
        if (_agent != null)
            _agent.enabled = false;
    }

    public Unit SelectTarget(bool _isPlayer)
    {
        if (_isPlayer)
            _targetList = BattleManager.Instance.EnemyArmy;
        else
            _targetList = BattleManager.Instance.PlayerArmy;

        float closest = Mathf.Infinity;
        UnitController target = null;

        //We avoid using the Vector3.Distance function to prevent the use of the expensive sqrt operation
        foreach (Unit enemy in _targetList)
        {
            if(_selfUnit.Settings.TargetMode == 0)
            {
                if (enemy != null && enemy.Alive)
                {
                    Vector3 directionToTarget = enemy.transform.position - transform.position;
                    float sqrtDistance = directionToTarget.sqrMagnitude;
                    if (sqrtDistance < closest)
                    {
                        closest = sqrtDistance;
                        target = enemy.GetComponent<UnitController>();
                    }
                }
            }
            else if(_selfUnit.Settings.TargetMode == 1)
            {
                if (enemy != null && enemy.Alive)
                {
                    int minHP = enemy.Health;
                    if (minHP < closest)
                    {
                        closest = minHP;
                        target = enemy.GetComponent<UnitController>();
                    }
                }
            }
        }

        if (target == null)
            return null;

        _target = target;


        return target.GetComponent<Unit>();
    }

    private void Update()
    {
        if(_target != null)
            MoveToTarget();
    }

    public void MoveToTarget()
    {
        if(_agent.enabled)
		{
            _agent.SetDestination(_target.transform.position);
            Quaternion targetRotation = Quaternion.LookRotation(_target.transform.position - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
        }
    }
}
