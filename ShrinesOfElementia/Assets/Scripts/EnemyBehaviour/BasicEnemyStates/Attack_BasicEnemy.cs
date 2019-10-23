using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyAttack")]
public class Attack_BasicEnemy : BasicEnemyBaseState
{

    private float attackDamage = 20;
    private float attackSpeed = 2.1f;
    private float cooldown;
    private float dodgeCooldown; // do some dodge cooldown timer too?

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }


    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering attack state.");
        cooldown = attackSpeed;
        Debug.Log("cooldown: " + cooldown);
        owner.Agent.SetDestination(owner.transform.position);
        
    }


    public override void Update()
    {

        base.Update();
        
        if (cooldown <= 0)
        {
            Attack();
        }
        cooldown -= Time.deltaTime;


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

    private void Attack()
    {
        Debug.Log("Attacking!!");
        cooldown = attackSpeed;
        // start strike animation, check hit, send damage event if hit. (in other script on GameObject that hit player or other creature )

        //owner.GetComponent<Animation_Test>().AttackAni();


        DamageEvent damageEvent = new DamageEvent("Enemy dealt " + attackDamage + " to player", (int)attackDamage, owner.gameObject, owner.Player.gameObject);
        EventSystem.Current.FireEvent(damageEvent);
    }

}
