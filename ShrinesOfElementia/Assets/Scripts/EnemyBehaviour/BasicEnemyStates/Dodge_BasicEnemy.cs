using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyDodge")]
public class Dodge_BasicEnemy : BasicEnemyBaseState
{
    private Vector3 newPosition;
    private float dodgeTimeout = 0.7f;
    private float countdown;

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("DODGE STATE");

        //set time
        countdown = dodgeTimeout;

        //calc new point
        newPosition = owner.transform.right * Random.Range(1.2f, 1.7f);
        if (Random.Range(0f, 2f) < 1f)
        {
            newPosition *= -1f; //instead, dodge to (player) left. 
        }

        
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        countdown -= Time.deltaTime;
        if(countdown < 0)
        {
            owner.Transition<Chase_BasicEnemy>();
        }

        //move enemy
        owner.transform.position += newPosition * 4.5f * Time.deltaTime;
        owner.transform.LookAt(owner.Player.transform);
    }


    public override void Leave()
    {
        base.Leave();
    }


}
