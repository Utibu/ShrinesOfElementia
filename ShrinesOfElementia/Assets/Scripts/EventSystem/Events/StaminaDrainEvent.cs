using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaDrainEvent : DebugEvent
{
    public int DrainAmount { get; }

    public StaminaDrainEvent(string eventDescription, int drainAmount) : base(eventDescription)
    {
        DrainAmount = drainAmount;
    }
}
