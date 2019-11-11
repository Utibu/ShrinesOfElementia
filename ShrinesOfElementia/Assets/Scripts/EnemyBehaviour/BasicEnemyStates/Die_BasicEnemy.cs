// Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyDie")]
public class Die_BasicEnemy : BasicEnemyBaseState
{

    private float dyingTime = 1.0f;
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
        GameObject gameobject = owner.GetComponent<GameObject>();
        EventManager.Current.FireEvent(new EnemyDeathEvent(gameobject, owner.SpawnArea));
        Destroy(owner.gameObject);

    }


    public override void HandleUpdate()
    {

        base.HandleUpdate();
        owner.transform.Rotate(new Vector3(1, 0, 0), 75);
        
        
    }


    public override void Leave()
    {
        base.Leave();
    }
}
