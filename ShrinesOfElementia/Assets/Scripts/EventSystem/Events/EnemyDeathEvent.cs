﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEvent : EventClass
{

    public GameObject Enemy;
    public GameObject SpawnArea;
    public bool Elite;
    public EnemyDeathEvent(GameObject Enemy, GameObject SpawnArea, bool Elite)
    {
        this.Enemy = Enemy;
        this.SpawnArea = SpawnArea;
        this.Elite = Elite;
    }


}
