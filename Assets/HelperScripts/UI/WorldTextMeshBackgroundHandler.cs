using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldTextMeshBackgroundHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text targetMesh = null;
    private SpriteRenderer spriteRenderer = null;
    private RectTransform targetRectTransform = null;
    [SerializeField] private float oversizeMultiplierX = 1.2f;
    [SerializeField] private float oversizeMultiplierY = 3f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetRectTransform = targetMesh.rectTransform;
        targetMesh.OnPreRenderText += TextChanged;
    }

    private void TextChanged(TMP_TextInfo obj)
    {
        this.spriteRenderer.size = new Vector2(targetRectTransform.rect.width* oversizeMultiplierX, targetRectTransform.rect.height* oversizeMultiplierY);
    }
}
