using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvent : DebugEvent
{

    public GameObject Instigator { get; }
    public bool EnteredCombat { get; }

    public CombatEvent(string eventDescription, GameObject instigator, bool enteredCombat) : base(eventDescription)
    {
        Instigator = instigator;
        EnteredCombat = enteredCombat;
    }
}
