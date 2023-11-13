using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySwitcher : MonoBehaviour
{
    private bool _dmgOrHeal;
    public GameObject DmgCard;
    public GameObject HealCard;


    public void SwitchAbility()
    {
        _dmgOrHeal = !_dmgOrHeal;

        if(!_dmgOrHeal)
        {
            DmgCard.SetActive(true);
            HealCard.SetActive(false);
            //Select the Damage Card
            PlayerInteractions.Instance.SetInteraction(0);
        }
        else
        {
            DmgCard.SetActive(false);
            HealCard.SetActive(true);
            //Select the Heal Card
            PlayerInteractions.Instance.SetInteraction(1);
        }
    }
    public bool DmgOrHeal { get => _dmgOrHeal; }
}
