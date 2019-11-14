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

    public Animator Animator { get; private set; }

    [SerializeField] private GameObject[] patrolPoints;
    public GameObject[] PatrolPoints { get => patrolPoints; }

    private EnemyAttack enemyAttack;

    public EnemyAttack EnemyAttack { get { return enemyAttack; } set { enemyAttack = value; } }

    //fix  this later
    public bool Elite;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        //Init Components.
        Agent = GetComponent<NavMeshAgent>();
        Agent.baseOffset = 0.5f;
        Player = Player.Instance;
        Animator = GetComponent<Animator>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();

        Physics.IgnoreLayerCollision(8, 4, false);
        //EventSystem.Current.RegisterListener<DamageEvent>(OnAttacked);

    }


    protected override void Update()
    {
        base.Update();

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

    public void DisableElite()
    {
        GetComponent<EnemyParticleController>().StopParticleSystem();
        Transition<Chase_BasicEnemy>();
        Elite = false;
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

    public void EnemyAttacked()
    {
        Animator.SetTrigger("IsHurt");
    }


}
