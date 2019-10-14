// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Text healthText;
    private int currentHealth, maxHealth;

    public int MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            CurrentHealth = maxHealth;
        }
    }

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            UpdateHealthBar();
        }
    }

    private void Start()
    {
        // Initiates the health values
        healthSlider.maxValue = maxHealth;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // Text setter
        healthText.text = CurrentHealth + "/" + MaxHealth;

        // Slider setter
        healthSlider.value = CurrentHealth;
    }
}
