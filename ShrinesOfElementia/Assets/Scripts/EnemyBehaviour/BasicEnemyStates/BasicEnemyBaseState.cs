﻿//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyBaseState : State
{
    
    //reference to statemachine of enemy this state belongs to.
    protected EnemySM owner;
    protected EnemyValues enemyValues;

    protected float distanceToPlayer;
    protected bool canSeePlayer;

    protected float damage;
    protected float atkCooldown;
    protected float attackRange;
    protected float sightRange;
    protected float speed;
    protected GameObject orb;
    protected float orbDropChance;
    protected float experienceAmount;
    

    //greater elemental values
    protected float castRange;
    


    //called from baseclass StateMachine in Awake().
    public override void Initialize(StateMachine stateMachine)
    {
        owner = (EnemySM)stateMachine;  // cast to subtype. 
        IntializeEnemyValues();
    }

    public void IntializeEnemyValues()
    {
        enemyValues = owner.GetComponent<EnemyValues>();

        //Set enemy values
        attackRange = enemyValues.AttackRange;
        sightRange = enemyValues.SightRange;
        atkCooldown = enemyValues.AtkCooldown;
        damage = enemyValues.Damage;
        castRange = enemyValues.CastRange;
        speed = enemyValues.Speed;
        orb = enemyValues.Orb;
        orbDropChance = enemyValues.OrbDropChance;
        experienceAmount = enemyValues.ExperienceAmount;

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

        //update distance to player
        distanceToPlayer = Vector3.Distance(owner.transform.position, owner.Player.transform.position);

        
    }

   
    
}
