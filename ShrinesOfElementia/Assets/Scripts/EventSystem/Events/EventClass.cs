// Author: Bilal El Medkouri

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EventClass
{
  
}

public class DebugEvent : EventClass
{
    public string eventDescription = "";
    public DebugEvent(string eventDescription)
    {
        this.eventDescription = eventDescription;
    }
}




/*      ///////OLD CODE///////
public abstract class Event<T> where T : Event<T>
{
    private bool hasFired;

    public delegate void EventListener(T info);
    public static event EventListener Listeners;

    public static void RegisterListener(EventListener listener)
    {
        Listeners += listener;
    }

    public static void UnregisterListener(EventListener listener)
    {
        Listeners -= listener;
    }

    public void FireEvent()
    {
        if (hasFired)
        {
            throw new System.Exception("This event has already fired, to prevent infinite loops, you can't refire an event.");
        }

        hasFired = true;

        Listeners?.Invoke(this as T);

        Debug.Log(this + " has fired");
    }
}
*/
