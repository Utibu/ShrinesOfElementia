using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyIdle")]
public class Idle_BasicEnemy : BasicEnemyBaseState
{
    private float idleTimerCountdown;
    private float idleTime = 7.0f;

    //called from baseclass StateMachine in Awake().
    public override void Initialize(StateMachine stateMachine) {
        base.Initialize(stateMachine);
    }


    public override void Enter() {
        Debug.Log("Entering idle state.");
        //set/reset timer each enter.
        
    }


    public override void Update() {

        //do idle stuff
        owner.transform.Rotate(new Vector3(0, 1, 0), 15 * Time.deltaTime);
        //owner.transform.position += Vector3.up * 10f * Time.deltaTime;
        //update idle timer
        idleTimerCountdown -= Time.deltaTime;

        // State Transition checks:

        if (distanceToPlayer <= 20)
        {
            Debug.Log("nu kommer jag och tar dig!");
            owner.Transition<Chase_BasicEnemy>();
        }
        else if (idleTimerCountdown <= 0)
        {
            idleTimerCountdown = idleTime;
            owner.Transition<Patrol_BasicEnemy>();
        }
    }


    public override void Leave()
    {
        Debug.Log("Leaving Idle state");
       

    }
}
