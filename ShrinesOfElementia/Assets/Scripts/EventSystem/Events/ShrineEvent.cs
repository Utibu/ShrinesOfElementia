using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineEvent : Event<ShrineEvent>
{
    public ShrineElementActivator Activator;


    public ShrineEvent(ShrineElementActivator activator)
    {
        Activator = activator;
    }
}
