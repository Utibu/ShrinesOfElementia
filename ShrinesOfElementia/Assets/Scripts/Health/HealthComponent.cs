// Author: Bilal El Medkouri
// Co-Authors: Joakim Ljung & Sofia Kauko

using UnityEngine;


public class HealthComponent : MonoBehaviour
{
    public bool IsInvulnerable { get { return isInvulnerable; } set { isInvulnerable = value; } }
    public GameObject Canvas { get { return canvas; } set { canvas = value; } }
    [SerializeField] private bool isInvulnerable;

    [Header("UI reference")]
    [SerializeField] private GameObject canvas;
    private HealthBarController healthBarController;

    [Header("Health attributes")]
    [SerializeField] protected int maxHealth;

    [Header("Level Up")]
    [SerializeField] private float baseHealthIncrease;
    [SerializeField] private float healthIncreasePerLevel;

    private Giant giant;
    bool isGiant;


    public int MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            CurrentHealth = maxHealth;
        }
    }

    private int currentHealth;
    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            //print(value);
            // Adding health

            if (value > MaxHealth)
            {
                currentHealth = MaxHealth;
            }
            else if (value > currentHealth)
            {
                currentHealth = value;
            }

            // Subtracting health

            else if (value < currentHealth)
            {
                if (IsInvulnerable == true) { }
                else if (value <= 0)
                {
                    currentHealth = 0;
                    Die();
                }
                else
                {
                    currentHealth = value;
                }
            }

            healthBarController.CurrentHealth = currentHealth;
        }
    }

    private void Awake()
    {
        //is this a giant? this gets a ref to giant statemachine, and ref to bosshealthbarcontroller is created.
        if (gameObject.TryGetComponent(out giant))
        {
            isGiant = true;
            Debug.Log("this a giant");
        }
       
    }

    private void Start()
    {
        // if gameobject is a giant, healthbarController is set a different way.
        if (isGiant)  
        {
            healthBarController = BossHealthBarController.Instance;
        }
        //else, its a player or enemy. They have canvases so set ref to component on canvas.
        else
        {
            healthBarController = canvas.GetComponent<HealthBarController>();
        }
        MaxHealth = maxHealth;
        IsInvulnerable = false;
        healthBarController.MaxHealth = maxHealth;

        if (gameObject.CompareTag("Player"))
        {
            EventManager.Instance.RegisterListener<LevelUpEvent>(OnLevelUp);
            BalancingManager.Instance.SetPlayerHealth(MaxHealth);
        }
    }

    private void OnLevelUp(LevelUpEvent eve)
    {
        if (gameObject.CompareTag("Player"))
        {
            float newMaxHealth = (float)MaxHealth;
            newMaxHealth += baseHealthIncrease;
            MaxHealth = (int)newMaxHealth;
            baseHealthIncrease += healthIncreasePerLevel;
        }
    }

    private void Die()
    {

        if (isGiant)
        {
            giant.Die();
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            GetComponent<EnemySM>().Transition<Die_BasicEnemy>();
        }
        else if (gameObject.CompareTag("Player"))
        {
            Debug.Log("Playerdied");
            EventManager.Instance.FireEvent(new PlayerDeathEvent("Player died", gameObject));
        }
    }



}
