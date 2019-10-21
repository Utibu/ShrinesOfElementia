using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    protected PlayerStateMachine owner;


    public override void Initialize(StateMachine stateMachine)
    {
        owner = (PlayerStateMachine)stateMachine;
    }

    public override void Update()
    {
        base.Update();
    }
}
