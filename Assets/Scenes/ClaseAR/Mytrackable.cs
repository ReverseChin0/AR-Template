using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vuforia;

public class Mytrackable : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour trackableBehaviour;
    public mitrackBehaviour miTracker;
    [SerializeField] GameObject miobj;
    LineRenderer miline = default;
    //public TextMeshPro mitext = default;
    void Start()
    {
        miTracker = FindObjectOfType<mitrackBehaviour>();
        miline = GetComponentInChildren<LineRenderer>();

        trackableBehaviour = GetComponent<TrackableBehaviour>();
        if (trackableBehaviour)
            trackableBehaviour.RegisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            encontrado();
        else
            perdido();
    }

    private void perdido()
    {
        miTracker.NotifyLost(miline);
    }

    private void encontrado()
    {
        miTracker.Notify(miobj.transform, miline);
    }
}
