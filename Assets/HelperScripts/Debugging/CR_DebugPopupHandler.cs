using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CR_DebugPopupHandler : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public float displayDuration = 5f;

    public void ShowPopup(string[] stringsToShow, bool autoClose = true)
    {
        // Combine the array of strings into a single string with new lines between them
        string combinedText = string.Join("\n", stringsToShow);

        // Set the combined text to the popup Text component
        popupText.SetText(combinedText);

        if (autoClose)
        {
            // Start the timer to close the popup automatically
            Invoke("ClosePopup", displayDuration);
        }
    }

    public void ClosePopup()
    {
        this.gameObject.SetActive(false);
    }
}
