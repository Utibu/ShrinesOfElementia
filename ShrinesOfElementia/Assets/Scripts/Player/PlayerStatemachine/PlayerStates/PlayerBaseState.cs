using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    protected PlayerStateMachine owner;
    protected CameraReference camera;

    public override void Initialize(StateMachine stateMachine)
    {
        owner = (PlayerStateMachine)stateMachine;
        camera = CameraReference.Instance;
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            owner.Transition<PlayerCombatState>();
        }
    }
}
