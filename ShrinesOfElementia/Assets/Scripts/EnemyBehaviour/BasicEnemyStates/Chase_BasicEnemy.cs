//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyChase")]
public class Chase_BasicEnemy : BasicEnemyBaseState
{

    private Vector3 startPosition;
    private float chaseSpeedModifier = 1.3f;
    //add eventual shouts/screehes from angry chasing enemy? yes please


    //called from baseclass StateMachine in Awake().
    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }


    public override void Enter()
    {
        Debug.Log("Entering chase state.");
        //modify agent to chase settings
        owner.Agent.speed *= chaseSpeedModifier;
        startPosition = owner.transform.position;

    }



    public override void Update()
    {
        base.Update();
        
        //do the chasing
        owner.Agent.SetDestination(owner.Player.transform.position);


        //State transition checks:
        if (distanceToPlayer >= 30)
        {
            owner.Transition<Idle_BasicEnemy>();
        }

    }

    public override void Leave()
    {
        Debug.Log("Leaving chase state");
        owner.Agent.SetDestination(startPosition);
    }
}
