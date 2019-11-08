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

    public override void Start()
    {
        BossEvents.Instance.OnBossFightAreaTriggerEnter += OnBossAreaEnter;
        print("Subscribed to BossFightAreaTriggerEnter");
    }

    private void OnBossAreaEnter()
    {
        print("BossAreaEntered");
        Transition<GiantPhaseOneState>();
    }

    private void OnDestroy()
    {
        BossEvents.Instance.OnBossFightAreaTriggerEnter -= OnBossAreaEnter;
    }

    private void StopAttacking()
    {
        Animator.SetBool("IsAttacking", false);
    }
}
