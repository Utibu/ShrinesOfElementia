// Author: Bilal El Medkouri

using UnityEngine;
using UnityEngine.AI;

// This needs to be picked apart. All of the cooldowns and damage values etc needs to be in separate scripts.
public class Giant : StateMachine
{
    // Components
    public static Giant Instance { get; private set; }
    public Animator Animator { get; set; }
    public NavMeshAgent Agent { get; set; }
    public HealthComponent HealthComponent { get; set; }

    // Bools for phases
    public bool Phase2Active { get; set; }
    public bool Phase3Active { get; set; }


    // Move all attacks to a manager of some sorts
    [Header("Basic Attack")]
    [SerializeField] protected int basicAttackDamage;
    public float BasicAttackRange;

    [Header("Sweep")]
    [SerializeField] protected int sweepDamage;
    public float SweepRange;
    [SerializeField] protected float sweepCooldown;

    #region SweepCooldownVariables
    private float sweepTimer;
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
    #endregion SweepCooldownVariables

    // [Header("Spawn")]

    [Header("Stomp")]
    public int StompDamage;
    public float StompRange;
    [SerializeField] protected float stompCooldown;

    #region StompCooldownVariables
    private float stompTimer;
    private bool stompAvailable;
    public bool StompAvailable
    {
        get => stompAvailable;
        set
        {
            stompAvailable = value;

            if (value == false)
            {
                stompTimer = stompCooldown;
            }
        }
    }
    #endregion StompCooldownVariables

    [Header("Leap")]
    public int LeapDamage;
    public float LeapRange;
    [SerializeField] protected float leapCooldown;

    #region LeapCooldownVariables
    private float leapTimer;
    private bool leapAvailable;
    public bool LeapAvailable
    {
        get => leapAvailable;
        set
        {
            leapAvailable = value;

            if (value == false)
            {
                leapTimer = leapCooldown;
            }
        }
    }
    #endregion LeapCooldownVariables

    protected override void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }

        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        HealthComponent = GetComponent<HealthComponent>();

        SweepAvailable = false;
        StompAvailable = false;
        LeapAvailable = false;

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

    // Move this to the manager mentioned above
    protected virtual void CountDownCooldowns()
    {
        // Sweep
        if (sweepAvailable == false && sweepTimer <= 0f)
        {
            SweepAvailable = true;
        }
        else if (sweepAvailable == false && sweepTimer > 0f)
        {
            sweepTimer -= Time.deltaTime;
        }

        // Stomp
        if (stompAvailable == false && stompTimer <= 0f)
        {
            StompAvailable = true;
        }
        else if (stompAvailable == false && stompTimer > 0f)
        {
            stompTimer -= Time.deltaTime;
        }

        // Leap
        if (leapAvailable == false && leapTimer <= 0f)
        {
            LeapAvailable = true;
        }
        else if (leapAvailable == false && leapTimer > 0f)
        {
            leapTimer -= Time.deltaTime;
        }
    }

    // This doesn't need to be here, since I've made a static reference to the giant
    protected virtual void OnBossAreaEnter()
    {
        print("BossAreaEntered");
        Transition<GiantPhaseOneState>();
        BossEvents.Instance.OnBossFightAreaTriggerEnter -= OnBossAreaEnter;
    }

    protected virtual void OnDestroy()
    {
        BossEvents.Instance.OnBossFightAreaTriggerEnter -= OnBossAreaEnter;
    }

    // Animation Manager?
    protected virtual void StopAttacking()
    {
        Animator.SetBool("IsAttacking", false);
    }

    // Not sure what to do with this. Maybe break out and create a reference instead.
    public virtual float DistanceToPlayer()
    {
        return Vector3.Distance(gameObject.transform.position, Player.Instance.transform.position);
    }
}
