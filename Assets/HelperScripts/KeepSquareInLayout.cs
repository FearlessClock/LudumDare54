 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class KeepSquareInLayout : MonoBehaviour
{
    private RectTransform rectTransform = null;
    private RectTransform layout = null;
    [SerializeField] private bool isHorizontal = true;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        layout = GetComponentInParent<LayoutGroup>().GetComponent<RectTransform>();
        UpdateSize();
    }

    private void UpdateSize()
    {
        if (isHorizontal)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.rect.height);
        }
        else
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectTransform.rect.width);
        }
        rectTransform.ForceUpdateRectTransforms();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
    }

    private void Update()
    {
        if(rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        if(layout == null)
        {
            layout = GetComponentInParent<LayoutGroup>().GetComponent<RectTransform>();
        }
        UpdateSize();
    }
}
