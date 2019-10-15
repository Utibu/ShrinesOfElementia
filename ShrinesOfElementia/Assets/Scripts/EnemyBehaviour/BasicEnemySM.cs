using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemySM : StateMachine
{

    private NavMeshAgent agent { get; set; }
    private GameObject player { get; set; }
    [SerializeField] private GameObject[] patrolPoints { get; set; }
    

    protected override void Awake()
    {
        base.Awake();
    } 

    public override void Start()
    {
        base.Start();

        
        //Init Components.
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    

    public override void Update()
    {
        base.Update();
        
    }


    //GETS AND SETS. 
    
    public NavMeshAgent getAgent()
    {
        return agent;
    }
    
}
