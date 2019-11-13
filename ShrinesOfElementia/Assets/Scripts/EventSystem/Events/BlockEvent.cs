// Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEvent : DebugEvent
{
    public float BlockAmount { get; }

    public BlockEvent(string description, float blockAmount) : base(description)
    {
        BlockAmount = blockAmount;
        //EventManager.Current.FireEvent(new StaminaDrainEvent("stamina drained", 20));
    }
}
