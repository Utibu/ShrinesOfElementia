//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAbilityEvent : DebugEvent
{
    public GameObject Target { get; }
    public Vector3 PointOfOrigin { get; }
    public float EffectRange { get; }

    public WindAbilityEvent(string eventDescription, GameObject target, Vector3 pointOfOrigin, float effectRange) : base(eventDescription)
    {
        Target = target;
        PointOfOrigin = pointOfOrigin;
        EffectRange = effectRange;
    }
}
