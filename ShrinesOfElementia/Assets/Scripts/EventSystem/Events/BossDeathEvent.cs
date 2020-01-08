//Author: Sofia Kauko
//Co-Author: Joakim Ljung
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathEvent : EnemyDeathEvent
{
    public string ElementType;
    public BossDeathEvent(string elementType, GameObject enemy, GameObject spawnarea, bool elite) :  base(enemy, spawnarea, elite)
    {
        ElementType = elementType;
        switch (elementType)
        {
            case "Fire":
                EventManager.Instance.FireEvent(new AchievementEvent("", "ItsGettingHotInHere"));
                break;
            case "Water":
                EventManager.Instance.FireEvent(new AchievementEvent("", "SurfsUp"));
                break;
            case "Wind":
                EventManager.Instance.FireEvent(new AchievementEvent("", "RockYouLikeAHurricane"));
                break;
            case "Earth":
                EventManager.Instance.FireEvent(new AchievementEvent("", "ILoveRockyRoad"));
                break;
        }
    }
    
}
