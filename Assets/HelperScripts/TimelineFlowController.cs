using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineFlowController : MonoBehaviour
{
    private PlayableDirector director = null;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void PauseTimeline()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void PlayTimeline()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
