using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEvent : DebugEvent
{
    public Collider Attacker { get; }
    public GameObject Defender { get; set; }

    public BlockEvent(string description, Collider attacker, GameObject defender) : base(description)
    {
        Attacker = attacker;
        Defender = defender;
    }
}
