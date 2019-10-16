//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyBaseState : State
{
    
    //reference to statemachine of enemy this state belongs to.
    protected BasicEnemySM owner;
    protected float distanceToPlayer;

    private float baseSpeed;

    //called from baseclass StateMachine in Awake().
    public override void Initialize(StateMachine stateMachine)
    {
        owner = (BasicEnemySM)stateMachine;  // cast to subtype. 
    }

    public virtual void Start()
    {
        baseSpeed = owner.Agent.speed;
    }

    public override void Enter()
    {
        Debug.Log("Entering base state.");


    }


    public override void Leave()
    {
        Debug.Log("Leaving base state");
        owner.Agent.speed = baseSpeed;
    }


    public override void Update()
    {
        distanceToPlayer = Vector3.Distance(owner.transform.position, owner.Player.transform.position);

    }
}
