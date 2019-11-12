//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyBaseState : State
{
    
    //reference to statemachine of enemy this state belongs to.
    protected EnemySM owner;

    protected float distanceToPlayer;
    protected bool canSeePlayer;

    protected float damage;
    protected float atkCooldown;
    protected float attackRange;
    protected float sightRange;

    //greater elemental values
    protected float castRange;
    


    //called from baseclass StateMachine in Awake().
    public override void Initialize(StateMachine stateMachine)
    {
        owner = (EnemySM)stateMachine;  // cast to subtype. 

        //Set enemy values
        attackRange = owner.GetComponent<EnemyValues>().AttackRange;
        sightRange = owner.GetComponent<EnemyValues>().SightRange;
        atkCooldown = owner.GetComponent<EnemyValues>().AtkCooldown;
        damage = owner.GetComponent<EnemyValues>().Damage;
        castRange = owner.GetComponent<EnemyValues>().CastRange;

    }

    public virtual void Start()
    {
        //EventManager.Current.RegisterListener<DamageEvent>(CheckDisadvantage);
    }

    public override void Enter()
    {

    }


    public override void Leave()
    {
        
    }


    public override void HandleUpdate()
    {

        //old
        distanceToPlayer = Vector3.Distance(owner.transform.position, owner.Player.transform.position);

        //replace with raycast 
        /*
        RaycastHit hit;
        Physics.Raycast(owner.transform.position, owner.Player.transform.position, out hit, sightRange, 8); // 5 = ignore UI elements, 8 = ignore enemy layer
        if (hit.transform != null && hit.transform.gameObject.CompareTag("Player"))
        {
            canSeePlayer = true;
        }
        else
        {
            canSeePlayer = false;
        }
        */

        

    }

    public void CheckDisadvantage(DamageEvent ev) // not used, yet. 
    {
        if (ev.InstigatorGameObject.CompareTag("Fire") && owner.gameObject.name.Equals("WindEliteEnemy"))
        {
            Debug.Log("wind elite struck by fire, disadvantage activated");
            owner.Elite = false;
            owner.Transition<Chase_BasicEnemy>();
            owner.GetComponent<ParticleSystem>().Pause();
        }
    }
    
}
