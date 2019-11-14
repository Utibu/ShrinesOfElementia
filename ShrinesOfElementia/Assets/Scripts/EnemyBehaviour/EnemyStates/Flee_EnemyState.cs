//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/Flee")]
public class Flee_EnemyState : BasicEnemyBaseState
{
    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }


    public override void Enter()
    {
        base.Enter();
        owner.Agent.speed *= 2;
        
    }


    public override void HandleUpdate()
    {

        base.HandleUpdate();
        //until player is far away enough, flee the other direction.
        if(Vector3.Distance(owner.transform.position, owner.Player.transform.position) < 5f)
        {
            owner.Agent.SetDestination(owner.Player.transform.right * -1.5f + owner.Player.transform.forward * 3f); ;
        }
        else
        {
            owner.Transition<Cast_EnemyState>();
        }

    }


    public override void Leave()
    {
        base.Leave();
        owner.Agent.speed = speed;
    }
}
