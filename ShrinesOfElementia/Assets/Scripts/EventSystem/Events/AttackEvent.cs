using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : DebugEvent
{
    //public float Damage { get; }

    public AttackEvent(string description) : base(description)
    {
        
        //EventManager.Current.FireEvent(new StaminaDrainEvent("stamina drained", 20));
    }
}
