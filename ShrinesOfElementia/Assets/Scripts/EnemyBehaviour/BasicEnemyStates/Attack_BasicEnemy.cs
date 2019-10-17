using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyAttack")]
public class Attack_BasicEnemy : BasicEnemyBaseState
{

    [SerializeField] private float attackDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackCooldown;
    private float countdown;
    [SerializeField] private float dodgeCooldown; // do some dodge cooldown timer too?

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }


    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering attack state.");
        countdown = attackCooldown;
        Debug.Log("countdown: " + countdown);
        owner.Agent.SetDestination(owner.transform.position);
        
    }


    public override void Update()
    {

        base.Update();
        
        if (countdown <= 0)
        {
            attack();
        }
        countdown -= Time.deltaTime;


        // state transition checks
        if (distanceToPlayer  > attackRange * 1.4f)
        {
            owner.Transition<Chase_BasicEnemy>();
        }

    }


    public override void Leave()
    {
        base.Leave();
        Debug.Log("Leaving attack state");
    }

    private void attack()
    {
        Debug.Log("Attacking!!");
        countdown = attackCooldown;
        // start strike animation, check hit, send damage event if hit. (in other script on GameObject that hit player or other creature )

        //owner.GetComponent<Animation_Test>().AttackAni();
        
    }

}
