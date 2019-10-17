//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemySM : StateMachine
{
    
    //Do not show the designers
    public NavMeshAgent Agent { get; private set; }

    public Player Player;

    [SerializeField] private GameObject[] patrolPoints;
    public GameObject[] PatrolPoints { get => patrolPoints; }
    

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
    }
 

    public override void Update()
    {
        base.Update();
        
    }


    
}
