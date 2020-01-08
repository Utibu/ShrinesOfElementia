//Author: Joakim Ljung
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEvent : DebugEvent
{
    public Vector3 SpawnPosition;

    public CheckpointEvent(string eventDescription, Vector3 spawnPosition) : base(eventDescription)
    {
        SpawnPosition = spawnPosition;
    }
}
