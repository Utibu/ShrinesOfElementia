// Author: Bilal El Medkouri
//Co-author: Niklas Almqvist

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEvent : DebugEvent
{
    public int Damage { get; }
    public GameObject InstigatorGameObject { get; }
    public GameObject TargetGameObject { get; }

    public bool IsAbility { get; }

    //public string DamageType { get; }

    public DamageEvent(string eventDescription, int damage, GameObject instigatorGameObject, GameObject targetGameObject, bool isAbility = false) : base(eventDescription)
    {
        Damage = damage;
        InstigatorGameObject = instigatorGameObject;
        TargetGameObject = targetGameObject;
        //DamageType = damageType;
        IsAbility = isAbility;
    }
}
