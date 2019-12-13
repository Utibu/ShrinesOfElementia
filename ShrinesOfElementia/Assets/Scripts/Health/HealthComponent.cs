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
    [SerializeField] private int maxHealth;

    [Header("Level Up")]
    [SerializeField] private float baseHealthIncrease;
    [SerializeField] private float healthIncreasePerLevel;


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
        healthBarController = canvas.GetComponent<HealthBarController>();
        healthBarController.MaxHealth = maxHealth;
        MaxHealth = maxHealth;
        IsInvulnerable = false;
    }

    private void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            EventManager.Instance.RegisterListener<LevelUpEvent>(OnLevelUp);
        }
    }

    private void OnLevelUp(LevelUpEvent eve)
    {
        float newMaxHealth = (float)MaxHealth;
        newMaxHealth += baseHealthIncrease;
        MaxHealth = (int)newMaxHealth;
        baseHealthIncrease += healthIncreasePerLevel;
    }

    private void Die()
    {

        if (gameObject.name == "EarthGiant" || gameObject.name == "FireGiant" || gameObject.name == "WindGiant" || gameObject.name == "WaterGiant")
        {

            GetComponent<Giant>().Die();
            
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            GetComponent<EnemySM>().Transition<Die_BasicEnemy>();
        }
        else if (gameObject.CompareTag("Player"))
        {
            Debug.Log("Playerdied");
            EventManager.Instance.FireEvent(new PlayerDeathEvent("Player died", gameObject));
            //SceneManager.LoadScene("DeathScreen");
        }
    }



}
