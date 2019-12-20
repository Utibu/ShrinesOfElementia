//Author Joakim Ljung
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEvent : DebugEvent
{
    public Vector3 RespawnLocation;

    public RespawnEvent(string eventDescription, Vector3 respawnLocation) : base(eventDescription)
    {
        RespawnLocation = respawnLocation;
    }
}
