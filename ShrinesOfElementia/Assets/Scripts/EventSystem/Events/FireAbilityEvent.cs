//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbilityEvent : DebugEvent
{
    public GameObject Target { get; }
    public Vector3 PointOfOrigin { get; }
    public float EffectRange { get; }

    public FireAbilityEvent(string eventDescription, GameObject target, Vector3 pointOfOrigin, float effectRange) : base(eventDescription)
    {
        Target = target;
        PointOfOrigin = pointOfOrigin;
        EffectRange = effectRange;
    }
}
