//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineEvent : DebugEvent
{
    public string Element;
    public Player Player;

    public ShrineEvent(string eventDescription, string element) : base(eventDescription)
    {
        Element = element;
        Player = Player.Instance;
    }
}
