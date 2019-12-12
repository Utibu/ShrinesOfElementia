using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/Stun_EnemyState")]
public class Stun_EnemyState : BasicEnemyBaseState
{
    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }


    public override void Enter()
    {
        base.Enter();
        owner.Agent.speed = 0f;
        TimerManager.Instance.SetNewTimer(owner.gameObject, 2f, EndStun);
    }


    public override void HandleUpdate()
    {

        //base.HandleUpdate();
        owner.transform.Rotate(new Vector3(1, 0, 0), 75);
    }


    public override void Leave()
    {
        base.Leave();
        owner.Agent.speed = speed;
        owner.transform.Rotate(new Vector3(1, 0, 0), -75);
    }


    private void EndStun()
    {
        owner.Transition<Idle_BasicEnemy>();
    }

}
