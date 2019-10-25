using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEvent : EventClass
{

    public GameObject Enemy;
    public GameObject SpawnArea; 
    public EnemyDeathEvent(GameObject Enemy, GameObject SpawnArea)
    {
        this.Enemy = Enemy;
        this.SpawnArea = SpawnArea;
    }


}
