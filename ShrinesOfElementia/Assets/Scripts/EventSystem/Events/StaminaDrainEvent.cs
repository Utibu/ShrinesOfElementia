//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeEvent : DebugEvent
{
    public float DrainAmount { get; }

    public DodgeEvent(string eventDescription, float drainAmount) : base(eventDescription)
    {
        DrainAmount = drainAmount;
    }
}
