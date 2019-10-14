using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testingEnemy : MonoBehaviour
{

    [SerializeField] private  GameObject[] patrolPoints;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //fetch agent.
        agent = this.GetComponent<NavMeshAgent>();
        //set agents target as first patrol point.
        agent.SetDestination(patrolPoints[0].GetComponent<Transform>().position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
