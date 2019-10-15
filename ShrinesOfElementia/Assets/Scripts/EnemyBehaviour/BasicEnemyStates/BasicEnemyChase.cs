using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyChase : BasicEnemyBaseState
{

  

    //called from baseclass StateMachine in Awake().
    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        //owner = stateMachine;
    }


    public override void Enter()
    {
        Debug.Log("Entering chase state.");
        owner.getAgent().SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position); // We should really define some gameobjects in basestate.. or something

    }


    public override void Leave()
    {
        Debug.Log("Leaving chase state");
    }


    public override void Update()
    {

    }
}
