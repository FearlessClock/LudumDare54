using HelperScripts.EventSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private EventObjectScriptable onGainScore;
    private int currentScore = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        onGainScore.AddListener(UpdateScore);
        UpdateScore(0);
    }

    private void UpdateScore(object obj)
    {
        currentScore += (int)obj ;
        scoreText.text = currentScore.ToString();
    }
}
