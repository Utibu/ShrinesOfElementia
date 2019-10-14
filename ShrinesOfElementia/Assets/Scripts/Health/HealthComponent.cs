// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
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

        }
    }
}
