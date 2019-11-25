//Author: Sofia Kauko
//Co-author: Niklas Almqvist
using UnityEngine;
using UnityEngine.AI;

public class EnemySM : StateMachine
{

    //Do not show the designers
    public NavMeshAgent Agent { get; private set; }
    public GameObject SpawnArea { get; private set; }
    public Player Player { get; private set; }
    public EnemyParticleController EnemyParticleController { get; private set; }
    public Animator Animator { get; private set; }
    public Collider Collider { get; private set; }
    

    [SerializeField] private GameObject[] patrolPoints;
    public GameObject[] PatrolPoints { get => patrolPoints; }

    public EnemyAttack EnemyAttack { get; set; }

    //fix  this later
    public bool Elite;
    public bool Disabled = false;

    //temporary timer. Create event based timer system or so maybeee
    [SerializeField] private float disabledTime;
    private float countdown;

    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        Agent.baseOffset = 0.5f;
        EnemyParticleController = GetComponent<EnemyParticleController>();
        Animator = GetComponent<Animator>();
        EnemyAttack = GetComponentInChildren<EnemyAttack>();
        Collider = GetComponent<Collider>();
    }

    protected override void Start()
    {
        base.Start();
        Player = Player.Instance;
        //Init Components.

        Physics.IgnoreLayerCollision(8, 4, false);
        //EventSystem.Current.RegisterListener<DamageEvent>(OnAttacked);
        EventManager.Current.RegisterListener<AttackEvent>(Dodge);
        EventManager.Current.RegisterListener<EarthAbilityEvent>(Stun);

    }


    protected override void Update()
    {
        base.Update();
        if (Disabled)
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0)
            {
                RestoreEnemy();
            }
        }
    }


    public void SetPatrolPoints(GameObject pp1, GameObject pp2)
    {
        patrolPoints[0] = pp1;
        patrolPoints[1] = pp2;
    }

    public void SetSpawnArea(GameObject SpawnArea)
    {
        this.SpawnArea = SpawnArea;
    }

    public void DisableEnemy()
    {
        Disabled = true;
        countdown = disabledTime;
        EnemyParticleController.StopParticleSystem();
        Transition<Chase_BasicEnemy>();
    }

    public void RestoreEnemy()
    {
        Disabled = false;
        EnemyParticleController.EnableParticleSystem();
        if (Elite)
        {
            Transition<Flee_EnemyState>();
        }
        else
        {
            Transition<Idle_BasicEnemy>();
        }
    }
    

    public void EnemyAttacked() // healtComponent better placement? EnemyValues? 
    {
        Animator.SetTrigger("IsHurt");
    }


    public void Dodge(AttackEvent ev)
    {
        float range = GetComponent<EnemyValues>().AttackRange;
        if (Random.Range(1f, 10f) > 6f && Vector3.Distance(gameObject.transform.position, Player.transform.position) < range)
        {
            if (!Elite)
            {
                Transition<Dodge_BasicEnemy>();
            }
            else
            {
                // flee transition here?
            }
        }
    }

    public void Stun(EarthAbilityEvent ev)
    {
        Transition<Stun_EnemyState>();
    }

    public void MoveAway()
    {
        Transition<Dodge_BasicEnemy>();
    }

    
    private void OnDestroy()
    {
        try
        {
            EventManager.Current.UnregisterListener<AttackEvent>(Dodge);
        }
        catch (System.NullReferenceException exception)
        {
            Debug.Log("Null reference exception caught, " + exception.StackTrace);
        }
        
    }

    // temporary placement for pushback.
    /*
    public void OnAttacked(DamageEvent ev)
    {

        if (ev.TargetGameObject.Equals(gameObject)) { 
            //pushback when attacked(damaged)
            
            Debug.Log("get pushed back");
            Vector3 newPosition = transform.position + ev.InstigatorGameObject.transform.forward * 4f;
            Agent.updateRotation = false;
            //Agent.speed *= 2;
            Agent.SetDestination(newPosition);

            transform.position += ev.InstigatorGameObject.transform.forward * 2f;
            
        }
        
    }
    */
}
