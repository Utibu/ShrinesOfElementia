// Author: Bilal El Medkouri
// Co-Authors: Joakim Ljung & Sofia Kauko

using UnityEngine;
using UnityEngine.SceneManagement;


public class HealthComponent : MonoBehaviour
{
    public bool IsInvulnerable { get { return isInvulnerable; } set { isInvulnerable = value; } }
    [SerializeField] private bool isInvulnerable;

    [Header("UI reference")]
    [SerializeField] private GameObject canvas;
    private HealthBarController healthBarController;

    [Header("Health attributes")]
    [SerializeField] private int maxHealth;
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
                    currentHealth = value;
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


    private void Die()
    {
        // Death event (?) Ful lösning bara för nu:
        if (gameObject.name == "Giant")
        {
            Giant.Instance.Animator.SetTrigger("Die");
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            GetComponent<EnemySM>().Transition<Die_BasicEnemy>();
        }
        else if (gameObject.CompareTag("Player"))
        {
            EventManager.Current.FireEvent(new PlayerDeathEvent("Player died", gameObject));
            //SceneManager.LoadScene("DeathScreen");
        }
    }



}
