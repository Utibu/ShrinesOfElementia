//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyAttack")]
public class Attack_BasicEnemy : BasicEnemyBaseState
{

    [SerializeField] private float basicAttackDamage = 20;
    [SerializeField]private float attackSpeed = 2.1f;
    private float cooldown;
    private float dodgeCooldown; // do some dodge cooldown timer too? a 10% chanse of dodging each PlayerDamage Event. (add forward force/vector of damaging obj to event) 

    

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        //EventSystem.Current.RegisterListener<DamageEvent>(OnAttacked);
    }


    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering attack state.");
        cooldown = 0;
        Debug.Log("cooldown: " + cooldown);
        owner.Agent.SetDestination(owner.transform.position);
        
    }


    public override void HandleUpdate()
    {

        base.HandleUpdate();

        if (cooldown <= 0)
        {
            Attack();
        }
        cooldown -= Time.deltaTime;


        

        // state transition checks
        if (distanceToPlayer  > attackRange * 2f)
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
        // rotate enemy towards target
        owner.Agent.updateRotation = true;
        owner.transform.LookAt(owner.Player.transform);

        cooldown = attackSpeed;

        owner.EnemyAttack.gameObject.SetActive(true);
        owner.Animator.SetTrigger("ShouldAttack");
    }

    
    /*
    private void OnAttacked(DamageEvent ev)
    {
        if (ev.TargetGameObject.Equals(owner.gameObject))
        {
            //pushback when attacked(damaged)
            Debug.Log("get pushed back");
            Vector3 newPosition = owner.transform.position += ev.InstigatorGameObject.transform.forward * 4f;
            owner.Agent.updateRotation = false;
            owner.Agent.SetDestination(newPosition);
        }
        
    }
    */

}
