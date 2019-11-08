// Author: Bilal El Medkouri

using UnityEngine;
using UnityEngine.AI;

public class Giant : StateMachine
{
    // Components
    public Animator Animator { get; set; }
    public NavMeshAgent Agent { get; set; }
    public HealthComponent HealthComponent { get; set; }

    protected override void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        HealthComponent = GetComponent<HealthComponent>();

        base.Awake();
    }
}
