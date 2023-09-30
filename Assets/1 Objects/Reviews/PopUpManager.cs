using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            popup.instance.Init(reviewInfo.allReview[Random.Range(0, reviewInfo.allReview.Count)]);
            popupShowing.Add(popup);
        }
        if(popupShowing.Count > 0)
        {
            UpdatePopups();
        }
    }

    [Button("spawnpopup")]
    public void SpawnPopup()
    {
        PopUp popup = new PopUp() ;
        popup.timer = popupShowTime;
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
    public ReviewPopupHandler instance;
    public float timer;
}