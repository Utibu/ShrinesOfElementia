using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InstantiateEnemyStates/BossIdle")]
public class BossIdle : BossBaseState
{
    
    
    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }


    public override void Enter()
    {
        //Debug.Log("Entering idle state.");
        base.Enter();
    }


    public override void HandleUpdate()
    {

        base.HandleUpdate();

    }

    public override void Leave()
    {
        base.Leave();
    }
}
