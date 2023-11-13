using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraTransition : MonoBehaviour
{
	public Transform MenuTransform;
	public Transform BattleTransform;
	public float lerpTime;

    private void Start()
    {
		TransitionTo(0);
    }
    public void TransitionTo(int _index)
	{
		transform.DOKill();
		if (_index == 0)
		{
			transform.DOMove(MenuTransform.position, lerpTime);
			transform.DORotate(MenuTransform.rotation.eulerAngles, lerpTime);
		}
		else if (_index == 1)
		{
			transform.DOMove(BattleTransform.position, lerpTime);
			transform.DORotate(BattleTransform.rotation.eulerAngles, lerpTime);
		}

	}
}
