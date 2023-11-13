using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;

public class ButtonClickAnimation : MonoBehaviour
{
	private Vector3 startingScale;
	public UnityEvent ExecuteAction;

	private void Start()
	{
		startingScale = transform.localScale;
	}

	public void Click()
	{
		transform.DOKill();
		transform.localScale = startingScale;
		transform.DOPunchScale(-Vector3.one * 0.2f, 0.1f).OnComplete(() => { ExecuteAction?.Invoke(); });
	}
}
