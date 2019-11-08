using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState : State
{
    protected EnemySM owner;
    public override void Initialize(StateMachine stateMachine)
    {
        owner = (EnemySM)stateMachine;
    }


    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Leave()
    {
        
    }
}
