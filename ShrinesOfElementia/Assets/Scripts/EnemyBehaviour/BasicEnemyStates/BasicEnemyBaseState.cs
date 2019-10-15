using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyBaseState : State
{
    
    //reference to statemachine of enemy this state belongs to.
    protected BasicEnemySM owner;

    //called from baseclass StateMachine in Awake().
    public override void Initialize(StateMachine stateMachine)
    {
        owner = (BasicEnemySM)stateMachine;  // cast to subtype. 
    }

    public void Start()
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Entering base state.");


    }


    public override void Leave()
    {
        Debug.Log("Leaving base state");
    }


    public override void Update()
    {

    }
}
