using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSizeEquilizer : MonoBehaviour
{
    private TextMeshProUGUI[] children = null;

    private void Start()
    {
        children = GetComponentsInChildren<TextMeshProUGUI>();
        ResetTextScaling();
    }

    public void ResetTextScaling()
    {
        if (children.Length > 0)
        {
            for (int i = 0; i < children.Length; i++)
            {
                children[i].enableAutoSizing = true;
                children[i].ForceMeshUpdate();
            }
        }
        EqualizeText();
    }

    public void EqualizeText()
    {
        if (children.Length > 0)
        {
            float smallest = children[0].fontSize;
            foreach (TextMeshProUGUI text in children)
            {
                if (smallest > text.fontSize)
                {
                    smallest = text.fontSize;
                }
            }
            for (int i = 0; i < children.Length; i++)
            {
                children[i].enableAutoSizing = false;
                children[i].fontSize = smallest;
            }
        }
    }
}