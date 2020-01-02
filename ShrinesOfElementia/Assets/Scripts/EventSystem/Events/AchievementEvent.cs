using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEvent : DebugEvent
{
    public string AchievementName;
    public AchievementEvent(string eventDescription, string achievementName) : base(eventDescription)
    {
        AchievementName = achievementName;
    }
}
