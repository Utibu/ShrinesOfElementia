// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public bool IsInvulnerable { get; set; }

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
        if (gameObject.CompareTag("Enemy"))
        {
            GetComponent<BasicEnemySM>().Transition<Die_BasicEnemy>();
        }
        else if (gameObject.CompareTag("Player"))
        {
            Debug.Log("YOU DIEDED");
        }
    }
}
