using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class VersionNumberText : MonoBehaviour
{
    private TextMeshProUGUI text = null;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.SetText("V "+Application.version);
    }
}
