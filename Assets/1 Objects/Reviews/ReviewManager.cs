using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewManager : MonoBehaviour
{
    public static ReviewManager Instance;
    private int lives = 5;

    private void Awake()
    {
        Instance = this;
    }

    [Button("review")]
    public void SendReview(int reviewScore = 3)
    {
        if(reviewScore < 3)
        {
            lives--;
            if(lives <= 0)
            {
                Debug.Log("lose");
            }
        }
        PopUpManager.Instance.SpawnPopup(5);
    }
}
