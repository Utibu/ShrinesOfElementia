//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathEvent : EnemyDeathEvent
{
    public string ElementType;
    public BossDeathEvent(string elementType, GameObject enemy, GameObject spawnarea, bool elite) :  base(enemy, spawnarea, elite)
    {
        ElementType = elementType;
    }
    
}
