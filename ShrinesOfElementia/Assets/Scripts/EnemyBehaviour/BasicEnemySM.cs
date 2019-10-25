//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemySM : StateMachine
{
    
    //Do not show the designers
    public NavMeshAgent Agent { get; private set; }
    public GameObject SpawnArea { get; private set; }
    public Player Player { get; private set; }
    public Animator Animator { get; private set; }

    [SerializeField] private GameObject[] patrolPoints;
    private EnemyAttack enemyAttack;
    public GameObject[] PatrolPoints { get => patrolPoints; }
    public EnemyAttack EnemyAttack { get { return enemyAttack; } set { enemyAttack = value; } }


    protected override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        //Init Components.
        Agent = GetComponent<NavMeshAgent>();
        Agent.baseOffset = 0.5f;
        Player = Player.Instance;
        Animator = GetComponent<Animator>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();
       
    }
 

    public override void Update()
    {
        base.Update();
        
    }


    public void SetPatrolPoints(GameObject pp1, GameObject pp2)
    {
        patrolPoints[0] = pp1;
        patrolPoints[1] = pp2;
    }

    public void setSpawnArea(GameObject SpawnArea)
    {
        this.SpawnArea = SpawnArea;
    }
    
}
