using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutAlphaTween : MonoBehaviour
{
    [SerializeField] private CanvasGroup group = null;
    [SerializeField] private float endPosition = 0;
    [SerializeField] private float time = 1;
    [SerializeField] private Ease ease = Ease.Flash;
    public void FadeOutAlpha(bool isInteractable = true)
    {
        group.interactable = isInteractable;
        group.DOFade(endPosition, time).SetId("FadeOutAlphaTween").SetUpdate(true).SetEase(ease);
    }

    private void OnDestroy()
    {
        group.DOKill();
    }
}
