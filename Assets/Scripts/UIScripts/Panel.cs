using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Panel : MonoBehaviour
{
	private bool _isOpen;

	public void Open()
	{
		if (!_isOpen)
		{
			transform.GetComponent<RectTransform>().DOAnchorPosX(0, 0.25f);
			_isOpen = true;
		}
	}
	public void Close()
	{
		if (_isOpen)
		{
			transform.GetComponent<RectTransform>().DOAnchorPosX(-1100, 0.25f).OnComplete(() => { gameObject.SetActive(false); }) ;
			_isOpen = false;
		}
	}
	public bool IsOpen { get => _isOpen; set => _isOpen = value; }
}
