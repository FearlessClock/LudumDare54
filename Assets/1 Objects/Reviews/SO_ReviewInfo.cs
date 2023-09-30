using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Reviews")]
public class SO_ReviewInfo : ScriptableObject
{
    public List<Review> allReview;
}

[Serializable]
public class Review
{
    public string title;
    public string description;
    [Range(0, 5)] public float stars;
}
