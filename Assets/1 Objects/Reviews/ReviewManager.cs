using HelperScripts.EventSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewManager : MonoBehaviour
{
    public static ReviewManager Instance;
    [SerializeField] private int lives = 5;
    [SerializeField] private EventScriptable onFileComplaint = null;
    bool hasLost = false;
    [SerializeField] private Image[] uiStars;

    private void Awake()
    {
        Instance = this;
        onFileComplaint.AddListener(SendReview);
    }

    [Button("review")]
    public void SendReview()
    {
        if (hasLost)
            return;

        int i = Random.Range(0, 3);
        lives--;
        UpdateLifes();
        if (lives <= 0)
        {
            Debug.Log("lose");
            hasLost = true;
            EndScreen.Instance.CallEndScreen(false);
        }
        PopUpManager.Instance.SpawnPopup(i);
    }

    private void UpdateLifes()
    {
        for (int i = 0; i < uiStars.Length; i++)
        {
            if (i < lives)
            {
                uiStars[i].color = Color.white;
            }
            else
            {
                uiStars[i].color = Color.clear;
            }
        }
    }


    private void OnDestroy()
    {
        onFileComplaint.RemoveListener(SendReview);
    }
}
