//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyBaseState : State
{
    
    //reference to statemachine of enemy this state belongs to.
    protected EnemySM owner;

    protected float distanceToPlayer;
    protected float attackRange = 1.3f;
    protected float sightRange = 18.0f;
    protected float castRange = 16f;



    //called from baseclass StateMachine in Awake().
    public override void Initialize(StateMachine stateMachine)
    {
        owner = (EnemySM)stateMachine;  // cast to subtype. 
        
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


    public override void HandleUpdate()
    {
        
        
        distanceToPlayer = Vector3.Distance(owner.transform.position, owner.Player.transform.position);

    }
}
