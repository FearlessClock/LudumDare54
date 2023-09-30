using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewPopupHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image stars;

    public void Init(Review review)
    {
        title.text = review.title;
        description.text = review.description;
        stars.fillAmount = review.stars / 5;
    }
}
