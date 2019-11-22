using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceEvent : DebugEvent
{
    public float Experience;

    public ExperienceEvent(string eventDescription, float experience) : base(eventDescription)
    {
        Experience = experience;
    }
}
