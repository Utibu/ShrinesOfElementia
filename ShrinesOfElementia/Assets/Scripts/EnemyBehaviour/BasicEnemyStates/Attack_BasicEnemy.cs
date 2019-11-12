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


    private bool pushPlayer;
    private float pushTimer = 0;
    

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

        
        Debug.DrawLine(owner.transform.position, owner.transform.position + owner.transform.up * 3);
        RaycastHit hit;
        if (Physics.Raycast(owner.transform.position, owner.transform.position + owner.transform.up * 3))
        {
            Debug.Log("something above enemy");
            owner.Player.GetComponent<MovementInput>().AddPush(owner.transform.forward * 20f);

            //owner.Player.transform.position += owner.transform.forward * 2f;
            //pushTimer = 1.3f;
        }

        /*
        if (pushTimer > 0)
        {
            pushTimer -= Time.deltaTime;
            owner.Player.transform.position += (owner.transform.forward * 20f) * Time.deltaTime;
        }
        */

        /*
        RaycastHit hit;
        Physics.Raycast(owner.transform.position, Vector3.up, out hit, 3f, 8);
        if (hit.transform != null && hit.transform.CompareTag("Player")) // om player är på enemy == true
        {
            //push off player + move aside
            owner.Player.GetComponent<MovementInput>().AddPush(owner.transform.forward * 2f);

            //owner.transform.position += owner.Player.transform.forward * 2f;
            Debug.Log("player är på enemy");
        }
        // seems like raycast is in a completely wrong direction and i ant see why. 
        // simpler version: 
        Debug.DrawRay(owner.transform.position, Vector3.up * 5f);
        if (Physics.Raycast(owner.transform.position, Vector3.up, 3f, 8)) // om player är på enemy == true
        {
            //push off player + move aside
            owner.Player.GetComponent<MovementInput>().AddPush(owner.transform.forward * 2f);

            //owner.transform.position += owner.Player.transform.forward * 2f;
            Debug.Log("player är på enemy");
        }
        */



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
        //owner.Agent.updateRotation = true;
        owner.transform.LookAt(owner.Player.transform);
        owner.transform.eulerAngles = new Vector3(0, owner.transform.eulerAngles.y, 0);

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
