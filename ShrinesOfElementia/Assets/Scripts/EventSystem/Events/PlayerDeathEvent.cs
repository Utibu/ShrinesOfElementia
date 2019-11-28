using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEvent : DebugEvent
{
    public GameObject Player;

    public PlayerDeathEvent(string eventDescription, GameObject player) : base(eventDescription)
    {
        Player = player;
    }
} 
