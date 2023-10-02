using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewPopupHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image[] stars;

    public void Init(Review review)
    {
        title.text = review.title;
        description.text = review.description;
        for (int i = 0; i < stars.Length; i++)
        {
            if(i < review.stars)
            {
                stars[i].color = Color.white;
            }
            else
            {
                stars[i].color = Color.clear;
            }
        }
    }
}
