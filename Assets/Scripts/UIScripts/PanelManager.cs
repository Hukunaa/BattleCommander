using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
	public List<Panel> Panels;
	private Panel _lastPanelOpen;

	public enum PANEL
	{
		MENU,
		PREBATTLE,
		BATTLE,
		POSTBATTLE
	}
	private void Start()
	{
		OpenPanel(0);
		GameManager.OnGameStateChanged += GameStateChanged;
	}

	void GameStateChanged(GameManager.GameStateType _state)
    {
		//Detect the end of a battle and opens the corresponding panel
		if (_state == GameManager.GameStateType.PostBattle)
			OpenPanel(3);
    }
	public void SetPanel(int _panel)
	{
		PANEL p = (PANEL)_panel;
		switch(p)
		{
			case PANEL.MENU:
				OpenPanel(0);
				GameManager.Instance.SetGameState(GameManager.GameStateType.Menu);
				break;
			case PANEL.PREBATTLE:
				OpenPanel(1);
				GameManager.Instance.SetGameState(GameManager.GameStateType.BattlePreparation);
				break;
			case PANEL.BATTLE:
				OpenPanel(2);
				GameManager.Instance.SetGameState(GameManager.GameStateType.Battle);
				break;
			case PANEL.POSTBATTLE:
				OpenPanel(3);
				GameManager.Instance.SetGameState(GameManager.GameStateType.PostBattle);
				break;
		}
	}

	void OpenPanel(int _index)
	{
		Panels[_index].gameObject.SetActive(true);
		Panels[_index].Open();

		if (_lastPanelOpen != null)
			_lastPanelOpen.Close();

		_lastPanelOpen = Panels[_index];

	}
}
