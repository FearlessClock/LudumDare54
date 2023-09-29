using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BounceTween : MonoBehaviour
{
    [SerializeField] private float maxBounceAmount = 0.03f;
    [SerializeField] private float timeToFullBounce = 1f;
    private void OnEnable()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1 + maxBounceAmount, timeToFullBounce).SetEase(Ease.Linear))
            .Append(transform.DOScale(1 - maxBounceAmount, timeToFullBounce*2).SetEase(Ease.Linear))
            .Append(transform.DOScale(1, timeToFullBounce).SetEase(Ease.Linear)).SetLoops(-1, LoopType.Restart).SetId("BounceTween");
    }

    private void OnDestroy()
    {
        DOTween.Kill("BounceTween");
    }
}
