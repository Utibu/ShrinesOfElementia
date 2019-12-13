using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaggerEvent : DebugEvent
{
    public GameObject Actor;

    public StaggerEvent(string eventDescription, GameObject actor) : base(eventDescription)
    {
        Actor = actor;
    }
}
