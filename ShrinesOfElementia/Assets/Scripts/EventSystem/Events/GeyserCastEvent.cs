//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserCastEvent : EventClass
{
    public Vector3 affectedPosition;
    public float effectRange;
    public GeyserCastEvent(Vector3 affectedPosition, float effectRange)
    {
        this.affectedPosition = affectedPosition;
        this.effectRange = effectRange;
    }
}
