using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyIdle : BasicEnemyBaseState
{
    

    //called from baseclass StateMachine in Awake().
    public override void Initialize(StateMachine stateMachine) {
        base.Initialize(stateMachine);
        //owner = stateMachine;
    }


    public override void Enter() {
        Debug.Log("Entering idle state.");
        
    }


    public override void Leave() {
        Debug.Log("Leaving Idle state");
    }


    public override void Update() {
        float distance = (owner.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude;
        if (distance < 100)
        {
            Debug.Log("nu kommer jag och tar dig!");
            owner.Transition<BasicEnemyChase>();
        }

    }
}
