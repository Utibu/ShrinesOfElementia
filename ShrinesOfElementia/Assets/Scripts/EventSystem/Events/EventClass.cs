// Author: Joakim Ljung

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EventClass
{
  
}

public class DebugEvent : EventClass
{
    public string eventDescription = "";
    public DebugEvent(string eventDescription)
    {
        this.eventDescription = eventDescription;
    }
}
