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
    protected float attackRange = 2.0f;
    protected float sightRange = 20.0f;



    //called from baseclass StateMachine in Awake().
    public override void Initialize(StateMachine stateMachine)
    {
        owner = (BasicEnemySM)stateMachine;  // cast to subtype. 
        
    }

    public virtual void Start()
    {
        
    }

    public override void Enter()
    {

    }


    public override void Leave()
    {
        
    }


    public override void Update()
    {
        
        
        distanceToPlayer = Vector3.Distance(owner.transform.position, owner.Player.transform.position);

    }
}
