using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class UnitDisplay : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    public MeshRenderer _hatRenderer;

	void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ReceiveHit()
    {
        transform.DOKill();
        transform.DOPunchScale(-Vector3.one * 0.2f, 0.1f);
    }
    public void UpdateUnitDisplay(UnitSettings settings, bool _playerSide)
	{
        switch(settings.Shape)
		{
            case UnitSettings.ShapeType.CUBE:
                _meshFilter.mesh = ResourcesManager.Instance.Meshes[ResourcesManager.Instance.Meshes.Keys.Single(s => s.Contains("Cube"))];
                break;
            case UnitSettings.ShapeType.SPHERE:
                _meshFilter.mesh = ResourcesManager.Instance.Meshes[ResourcesManager.Instance.Meshes.Keys.Single(s => s.Contains("Icosphere"))];
                break;
        }

        switch (settings.Color)
        {
            case UnitSettings.ColorType.BLUE:
                _meshRenderer.material.color = Color.cyan;
                break;
            case UnitSettings.ColorType.RED:
                _meshRenderer.material.color = Color.red;
                break;
            case UnitSettings.ColorType.GREEN:
                _meshRenderer.material.color = Color.green;
                break;
        }

        switch(settings.Size)
		{
            case UnitSettings.SizeType.BIG:
                gameObject.transform.localScale = Vector3.one * 1.1f;
                break;
            case UnitSettings.SizeType.SMALL:
                gameObject.transform.localScale = Vector3.one;
                break;
        }

        if(_hatRenderer != null)
        {
            if (_playerSide)
                _hatRenderer.material.color = new Color(0, 0.5f, 1.0f);
            else
                _hatRenderer.material.color = Color.red;
        }
    }
}
