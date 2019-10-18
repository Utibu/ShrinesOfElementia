using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineEvent : Event<ShrineEvent>
{
    public string Element;
    public Player Player;

    public ShrineEvent(string element)
    {
        Element = element;
        Player = Player.Instance;
    }
}
