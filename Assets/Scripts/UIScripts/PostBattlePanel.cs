using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostBattlePanel : MonoBehaviour
{
    public GameObject WinIcon;
    public GameObject LooseIcon;

    private void OnEnable()
    {
        if (BattleManager.Instance.LastBattleResult == false)
            ShowLoose();
        else if (BattleManager.Instance.LastBattleResult == true)
            ShowWin();
    }

    void ShowWin()
    {
        WinIcon.SetActive(true);
        LooseIcon.SetActive(false);
    }

    void ShowLoose()
    {
        WinIcon.SetActive(false);
        LooseIcon.SetActive(true);
    }
}
