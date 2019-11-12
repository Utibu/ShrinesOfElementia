// Author: Bilal El Medkouri

using UnityEngine;
using UnityEngine.AI;

public class Giant : StateMachine
{
    // Components
    public Animator Animator { get; set; }
    public NavMeshAgent Agent { get; set; }
    public HealthComponent HealthComponent { get; set; }

    [Header("Basic Attack")]
    [SerializeField] private int basicAttackDamage;
    [SerializeField] private float basicAttackRange;

    [Header("Sweep")]
    [SerializeField] private int sweepDamage;
    [SerializeField] private float sweepRange, sweepCooldown;
    private float sweepTimer;

    // [Header("Spawn")]

    private bool sweepAvailable;
    public bool SweepAvailable
    {
        get => sweepAvailable;
        set
        {
            sweepAvailable = value;

            if (value == false)
            {
                sweepTimer = sweepCooldown;
            }
        }
    }

    protected override void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        HealthComponent = GetComponent<HealthComponent>();

        sweepTimer = sweepCooldown;
        SweepAvailable = false;

        base.Awake();
    }

    protected override void Start()
    {
        BossEvents.Instance.OnBossFightAreaTriggerEnter += OnBossAreaEnter;
        print("Subscribed to BossFightAreaTriggerEnter");
    }

    protected override void Update()
    {
        CountDownCooldowns();

        base.Update();
    }

    protected void CountDownCooldowns()
    {
        print("Is sweep available? " + sweepAvailable);
        print("Sweep timer: " + sweepTimer);
        // Sweep
        if (sweepAvailable == false && sweepTimer <= 0f)
        {
            SweepAvailable = true;
        }
        else if (sweepAvailable == false && sweepTimer > 0f)
        {
            sweepTimer -= Time.deltaTime;
        }
    }

    private void OnBossAreaEnter()
    {
        print("BossAreaEntered");
        Transition<GiantPhaseOneState>();
        BossEvents.Instance.OnBossFightAreaTriggerEnter -= OnBossAreaEnter;
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
