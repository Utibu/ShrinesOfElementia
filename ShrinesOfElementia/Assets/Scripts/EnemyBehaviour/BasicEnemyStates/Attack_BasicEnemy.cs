//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyAttack")]
public class Attack_BasicEnemy : BasicEnemyBaseState
{

    
    private float cooldown;
    private float dodgeCooldown; // do some dodge cooldown timer too? a 10% chanse of dodging each PlayerDamage Event. (add forward force/vector of damaging obj to event) 
    
    

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        
    }


    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering attack state.");
        cooldown = 0;
        Debug.Log("cooldown: " + cooldown);
        //owner.Agent.SetDestination(owner.transform.position);
        owner.Agent.isStopped = true;
    }


    public override void HandleUpdate()
    {

        base.HandleUpdate();
        cooldown -= Time.deltaTime;

        if (cooldown <= 0 && distanceToPlayer <= attackRange)
        {
           
            Attack();
            
            
        }
        /*
        else if (cooldown <= 0 && distanceToPlayer > attackRange) // too far away to strike, close in to target instead
        {
            owner.transform.LookAt(owner.Player.transform);
            owner.transform.eulerAngles = new Vector3(0, owner.transform.eulerAngles.y, 0);
            owner.transform.position += owner.transform.forward * 2.5f * Time.deltaTime;
        }
        */
        else if(owner.GetComponent<EnemyValues>().GoBack == false && distanceToPlayer > attackRange)
        {
            owner.transform.LookAt(owner.Player.transform);
            owner.transform.eulerAngles = new Vector3(0, owner.transform.eulerAngles.y, 0);
            owner.transform.position += owner.transform.forward * 6f * Time.deltaTime;
            //owner.transform.position += owner.transform.forward * 10.5f * Time.deltaTime;
        }
        else if (owner.GetComponent<EnemyValues>().GoBack == true) // temporary until i find away to see from state is animation has finished/ gone halfay and so on.
        {
            owner.transform.position += owner.transform.forward * -2.5f * Time.deltaTime;
        }

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
        owner.Agent.isStopped = false;
    }

    private void Attack()
    {
        
        Debug.Log("Attacking!!");
        owner.transform.LookAt(owner.Player.transform);
        owner.transform.eulerAngles = new Vector3(0, owner.transform.eulerAngles.y, 0);

        cooldown = atkCooldown;

        owner.EnemyAttack.gameObject.SetActive(true);
        owner.Animator.SetTrigger("ShouldAttack");
    }


    

}
