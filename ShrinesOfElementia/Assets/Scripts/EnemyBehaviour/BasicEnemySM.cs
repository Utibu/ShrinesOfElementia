//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemySM : StateMachine
{
    
    //Do not show the designers
    public NavMeshAgent Agent { get; private set; }
    public GameObject[] patrolPoints;

    //Actually, you could just remove this SM subclass alltogether. If there are no type unique variables that needs to be stored here. 

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        //Init Components.
        Agent = GetComponent<NavMeshAgent>();

    }
 

    public override void Update()
    {
        base.Update();
        
    }


    
}
