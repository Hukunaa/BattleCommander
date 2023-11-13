using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UnitCounter : MonoBehaviour
{
    public TextMeshProUGUI _text;
    public bool _isPlayer;
    public Slider _slider;

    private int _maxHp;

    private void OnEnable()
    {
        SetMaxHP();
    }
    void SetMaxHP()
    {
        _maxHp = _isPlayer ? 
            BattleManager.Instance.PlayerArmy.Where(c => c.Alive).Sum(u => u.Settings.Health) : 
            BattleManager.Instance.EnemyArmy.Where(c => c.Alive).Sum(u => u.Settings.Health);
    }
    private void Update()
    {
        int unitCount = _isPlayer ? BattleManager.Instance.PlayerArmy.Where(c => c.Alive).Count() : BattleManager.Instance.EnemyArmy.Where(c => c.Alive).Count();
        int hpCount = _isPlayer ? 
            BattleManager.Instance.PlayerArmy.Where(c => c.Alive).Sum(u => u.Health) : 
            BattleManager.Instance.EnemyArmy.Where(c => c.Alive).Sum(u => u.Health);
        _text.text = unitCount.ToString();
        _slider.value = (float)hpCount / (float)_maxHp;
    }
}
