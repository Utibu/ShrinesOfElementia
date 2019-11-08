using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State : ScriptableObject
{
    public virtual void Initialize(StateMachine stateMachine) { }
    public virtual void Enter() { }
    public virtual void Leave() { }
    public virtual void Update() { }
}

