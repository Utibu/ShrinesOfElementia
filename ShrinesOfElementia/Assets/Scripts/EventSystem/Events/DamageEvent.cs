// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEvent : DebugEvent
{
    public int Damage { get; }
    public GameObject InstigatorGameObject { get; }
    public GameObject TargetGameObject { get; }

    //public string DamageType;

    public DamageEvent(string eventDescription, int damage, GameObject instigatorGameObject, GameObject targetGameObject) : base(eventDescription)
    {
        Damage = damage;
        InstigatorGameObject = instigatorGameObject;
        TargetGameObject = targetGameObject;
        //DamageType = damageType;
    }
}
