using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{
    [HideInInspector]
    public int SizeX;
    [HideInInspector]
    public int SizeY;
    [HideInInspector]
    public int Scale;

    private List<Vector3> _spotPositions = new List<Vector3>();
    private List<GameObject> _slots = new List<GameObject>();
    public GameObject _slotPrefab;

    void Start()
    {
        GameManager.OnGameStateChanged += ShowHideGrid;
        for (int i = 0; i < 40; ++i)
        {
            GameObject slot = Instantiate(_slotPrefab, transform.position, transform.rotation);
            slot.SetActive(false);
            _slots.Add(slot);
        }
        if (_spotPositions.Count == 0)
            GenerateGrid();
    }
    public void GenerateGrid()
    {
        _spotPositions.Clear();
        for (int i = 0; i < SizeX; ++i)
            for(int j = 0; j < SizeY; ++j)
                _spotPositions.Add(new Vector3(i, 0, j));

    }

    public void ShowHideGrid(GameManager.GameStateType _state)
    {
        if (_state == GameManager.GameStateType.BattlePreparation)
            VisualizeGrid(true);
        else
            VisualizeGrid(false);
    }

    public Vector3 GetPosition(int _index)
    {
        if (_index >= _spotPositions.Count)
        {
            Debug.LogError("Invalid position asked to the grid");
            return Vector3.zero;
        }

        return (transform.position + (SpotPositions[_index] * Scale)) - new Vector3((SizeX - 1) * Scale * 0.5f, 0, (SizeY - 1) * Scale * 0.5f);
    }

    private void OnDestroy()
    {
        _slots.ForEach(s => Destroy(s));
        GameManager.OnGameStateChanged -= ShowHideGrid;
    }
    public void VisualizeGrid(bool _show)
    {
        if (_show)
        {
            _slots.ForEach(s => s.SetActive(false));
            for (int i = 0; i < SpotPositions.Count; ++i)
            {
                _slots[i].SetActive(true);
                _slots[i].transform.position = GetPosition(i) - Vector3.up * 0.49f;
            }
        }
        else
            _slots.ForEach(s => s.SetActive(false));
    }
    public int SizeXY { get => SizeX * SizeY; }
    public List<Vector3> SpotPositions { get => _spotPositions;}
}
