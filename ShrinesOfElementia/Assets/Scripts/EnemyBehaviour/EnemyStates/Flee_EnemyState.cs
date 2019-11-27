//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/Flee")]
public class Flee_EnemyState : BasicEnemyBaseState
{
    private float fleeTimer = 2f;
    private float countdown;

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }


    public override void Enter()
    {
        base.Enter();
        owner.Agent.speed *= 2f;
        owner.Agent.SetDestination(owner.Player.transform.right * -1.5f + owner.Player.transform.forward * 3f);
        countdown = fleeTimer;
    }


    public override void HandleUpdate()
    {

        base.HandleUpdate();
        countdown -= Time.deltaTime;

        //until player is far away enough, flee the other direction.
        if(countdown <= 0 || Vector3.Distance(owner.transform.position, owner.Player.transform.position) > 6f)
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
