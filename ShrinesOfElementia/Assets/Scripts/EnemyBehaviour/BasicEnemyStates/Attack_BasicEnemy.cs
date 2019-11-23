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

    private void testTimer()
    {
        Debug.Log("TIMER WORKS");
    }

    public override void HandleUpdate()
    {

        base.HandleUpdate();
        cooldown -= Time.deltaTime;

        //is player facing enemy?
        Vector3 direction = owner.transform.position - owner.Player.transform.position;
        float angle = Vector3.Angle(direction, owner.Player.transform.forward);

        if (cooldown <= 0 && distanceToPlayer <= attackRange)
        {
           
            Attack();
            Timer timer = new Timer(3, testTimer);
            
        }
        /*
        else if (cooldown <= 0 && distanceToPlayer > attackRange) // too far away to strike, close in to target instead
        {
            owner.transform.LookAt(owner.Player.transform);
            owner.transform.eulerAngles = new Vector3(0, owner.transform.eulerAngles.y, 0);
            owner.transform.position += owner.transform.forward * 2.5f * Time.deltaTime;
        }
        */
        else if (owner.GetComponent<EnemyValues>().GoBack == true && distanceToPlayer < attackRange * 1.5f)
        {
            owner.transform.position += owner.transform.forward * -2.9f * Time.deltaTime;
        }
        else if (angle < 140 && owner.GetComponent<EnemyValues>().GoBack == false && distanceToPlayer > attackRange)
        {
            owner.transform.LookAt(owner.Player.transform);
            owner.transform.eulerAngles = new Vector3(0, owner.transform.eulerAngles.y, 0);
            owner.transform.position += owner.transform.forward * 3.5f * Time.deltaTime;
        }
        else if(owner.GetComponent<EnemyValues>().GoBack == false && distanceToPlayer > attackRange)
        {
            owner.transform.LookAt(owner.Player.transform);
            owner.transform.eulerAngles = new Vector3(0, owner.transform.eulerAngles.y, 0);
            owner.transform.position += owner.transform.forward * 6f * Time.deltaTime;
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
