using HelperScripts.EventSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewManager : MonoBehaviour
{
    public static ReviewManager Instance;
    [SerializeField] private int lives = 5;
    [SerializeField] private EventScriptable onFileComplaint = null;

    private void Awake()
    {
        Instance = this;
        onFileComplaint.AddListener(SendReview);
    }

    [Button("review")]
    public void SendReview()
    {
        int i = Random.Range(0, 3);
        lives--;
        if (lives <= 0)
        {
            Debug.Log("lose");
        }
        PopUpManager.Instance.SpawnPopup(i);
    }
}
