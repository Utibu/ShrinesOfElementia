// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEvent : Event<DamageEvent>
{
    public int Damage { get; }
    public GameObject InstigatorGameObject { get; }
    public GameObject TargetGameObject { get; }

    public DamageEvent(int damage, GameObject instigatorGameObject, GameObject targetGameObject)
    {
        Damage = damage;
        InstigatorGameObject = instigatorGameObject;
        TargetGameObject = targetGameObject;
    }
}
