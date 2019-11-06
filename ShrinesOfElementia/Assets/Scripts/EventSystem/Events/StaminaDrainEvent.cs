using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaDrainEvent : DebugEvent
{
    public float DrainAmount { get; }

    public StaminaDrainEvent(string eventDescription, float drainAmount) : base(eventDescription)
    {
        DrainAmount = drainAmount;
    }
}
