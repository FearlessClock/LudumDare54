using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CR_TikTokScaler : MonoBehaviour
{
    private const float TargetAspectRatio = 9f / 16f;
    [SerializeField] private float oversize = 2;

    private void OnEnable()
    {
        ResizeToTargetAspectRatio();
    }

    private void Update()
    {
        // Uncomment the line below if you want the image to resize dynamically as the screen size changes.
        // ResizeToTargetAspectRatio();
    }

    private void ResizeToTargetAspectRatio()
    {
        // Get the current screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Debug.Log(screenWidth + " | " + screenHeight);
        float targetHeight = screenHeight;
        float targetWidth = screenHeight * TargetAspectRatio;

        // Set the new size for the image GameObject
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth+ oversize);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight+ oversize);
    }
}
