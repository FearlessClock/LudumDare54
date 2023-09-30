using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopUpManager : MonoBehaviour
{

    public static PopUpManager Instance;
    [SerializeField] private ReviewPopupHandler popupPrefab;
    [SerializeField] private Transform spawnParent;
    private Queue<PopUp> popupQueue;
    [SerializeField] private float popupShowTime;
    [SerializeField] private int numberOfVisiblePopup;
    private List<PopUp> popupShowing;
    [SerializeField] private SO_ReviewInfo reviewInfo;

    private void Awake()
    {
        Instance = this;
        popupShowing = new List<PopUp>();
        popupQueue = new Queue<PopUp>();
    }


    private void Update()
    {
        if(popupShowing.Count < numberOfVisiblePopup && popupQueue.Count > 0)
        {
            PopUp popup = popupQueue.Dequeue();
            popup.instance = Instantiate(popupPrefab, spawnParent);
            popup.instance.Init(popup.review);
            popupShowing.Add(popup);
        }
        if(popupShowing.Count > 0)
        {
            UpdatePopups();
        }
    }

    public void SpawnPopup(int reviewScore = 5)
    {
        PopUp popup = new PopUp() ;
        popup.timer = popupShowTime;
        var reviews = reviewInfo.allReview.FindAll(x => x.stars == reviewScore).ToList();
        popup.review = reviews[Random.Range(0, reviews.Count)];
        popupQueue.Enqueue(popup);
    }

    private void UpdatePopups()
    {
        for (int i = 0; i < popupShowing.Count; i++)
        {
            PopUp popUp = popupShowing[i];
            popUp.timer -= Time.deltaTime;
            if (popUp.timer <= 0)
            {
                popupShowing.RemoveAt(i);
                Destroy(popUp.instance.gameObject);
            }
        }
    }
}

public class PopUp
{
    public Review review;
    public ReviewPopupHandler instance;
    public float timer;
}