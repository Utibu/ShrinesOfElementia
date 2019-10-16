//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemySM : StateMachine
{
    //Let designers fiddle with
    [SerializeField]private GameObject[] patrolPoints;
    public GameObject[] PatrolPoints { get { return PatrolPoints; } set { PatrolPoints = patrolPoints; } }
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int attackDamage;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int attackCooldown;
    [SerializeField] private int dodgeCooldown;
    

    //Do not show the designers
    public NavMeshAgent Agent { get; private set; }
    public GameObject Player { get; private set; }



    protected override void Awake()
    {
        base.Awake();
    } 

    public override void Start()
    {
        base.Start();
        //Init Components.
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindWithTag("Player");
        
    }
 

    public override void Update()
    {
        base.Update();
        
    }


    
}
