using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    public Animator WeaponAnimator;

    public void Play()
    {
        WeaponAnimator.ResetTrigger("Hit");
        WeaponAnimator.SetTrigger("Hit");
    }

}
