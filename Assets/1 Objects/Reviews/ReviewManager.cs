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
    bool hasLost = false;
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
        Debug.Log("file complaint recieved");

        int i = Random.Range(0, 3);
        lives--;
        if (lives <= 0)
        {
            Debug.Log("lose");
            hasLost = true;
        }
        PopUpManager.Instance.SpawnPopup(i);
    }

    private void OnDestroy()
    {
        onFileComplaint.RemoveListener(SendReview);
    }
}
