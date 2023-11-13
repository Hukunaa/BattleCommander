using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextHovering : MonoBehaviour
{
    public int Amplitude;
    public int Speed;
    void Start()
    {
        transform.DOMoveY(transform.position.y + (1 * Amplitude), Speed)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutQuad).SetDelay(Random.value);
    }
}
