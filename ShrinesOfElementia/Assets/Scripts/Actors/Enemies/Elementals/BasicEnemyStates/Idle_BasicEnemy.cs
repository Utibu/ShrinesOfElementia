using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyIdle")]
public class Idle_BasicEnemy : BasicEnemyBaseState
{
    //let enemy idle at each patrol point, for x seconds or until player is close.
    private float idleTimerCountdown;
    private float idleTime = 4.0f;


    //called from baseclass StateMachine in Awake().
    public override void Initialize(StateMachine stateMachine) {
        base.Initialize(stateMachine);
    }


    public override void Enter() {
        Debug.Log("Entering idle state.");
        base.Enter();
    }


    public override void HandleUpdate() {

        base.HandleUpdate();

        //do idle stuff (placeholder til we have animations)
        owner.transform.Rotate(new Vector3(0, 1, 0), 35 * Time.deltaTime);


        //update idle timer
        idleTimerCountdown -= Time.deltaTime;

        // State Transition checks:
        if (distanceToPlayer < sightRange)
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
        base.Leave();
        Debug.Log("Leaving Idle state");
        idleTimerCountdown = idleTime;

    }
}
