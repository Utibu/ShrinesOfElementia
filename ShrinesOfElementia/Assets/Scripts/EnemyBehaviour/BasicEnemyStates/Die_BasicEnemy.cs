using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyDie")]
public class Die_BasicEnemy : BasicEnemyBaseState
{

    private float dyingTime = 1.5f;
    private float timer;


    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }


    public override void Enter()
    {
        base.Enter();
        timer = dyingTime;
        owner.transform.position += Vector3.down * 1.2f;

        // Start some cool death animation
        // play wicked screech of anguish

    }


    public override void Update()
    {

        base.Update();
        owner.transform.Rotate(new Vector3(1, 0, 0), 75);
        

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            owner.gameObject.SetActive(false);
        }

    }


    public override void Leave()
    {
        base.Leave();
    }
}
