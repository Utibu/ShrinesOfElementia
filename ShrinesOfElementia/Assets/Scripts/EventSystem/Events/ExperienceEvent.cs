using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceEvent : DebugEvent
{
    public int Experience;

    public ExperienceEvent(string eventDescription, int experience) : base(eventDescription)
    {
        Experience = experience;
    }
}
