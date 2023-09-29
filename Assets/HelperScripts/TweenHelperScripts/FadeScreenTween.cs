using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeScreenTween : MonoBehaviour
{
    [SerializeField] private float totalFadeTime = 1;
    [SerializeField] private float outOfScreenPosition = 0;
    public StringEvent OnFadeEvent;
    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(((RectTransform) transform).DOLocalMoveY(-outOfScreenPosition, 0))
            .Append(((RectTransform)transform).DOLocalMoveY(0, totalFadeTime/2).SetEase(Ease.Linear))
            .AppendCallback(() => OnFadeEvent?.Invoke("FADEHALFWAY")).SetId("FadeScreenTween");
    }

    public void FinishFade()
    {
        DOTween.Kill("FadeScreenTween");
        Sequence sequence = DOTween.Sequence();
        sequence.Append(((RectTransform)transform).DOLocalMoveY(outOfScreenPosition, totalFadeTime / 2).SetEase(Ease.Linear))
            .AppendCallback(() => OnFadeEvent?.Invoke("FADEDONE")).SetId("FadeScreenTween");
    }

    private void OnDestroy()
    {
        DOTween.Kill("FadeScreenTween");
    }
}
