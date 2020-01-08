// Author: Bilal El Medkouri
// Co-Author: Joakim Ljung, Sofia Chyle Kauko

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected Slider healthSlider;
    [SerializeField] protected Image healthImage;
    [SerializeField] protected Text healthText;
    protected int currentHealth, maxHealth;




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

    private void Awake()
    {
        // Initiates the health values
        InitiateHealthValues(MaxHealth);
        
    }

    private void UpdateHealthBar()
    {
        // Text setter
        healthText.text = CurrentHealth + "/" + MaxHealth;

        // Slider setter
        if(healthSlider != null)
        {
            healthSlider.value = CurrentHealth;
        }
        else
        {
            healthImage.fillAmount = CurrentHealth / (float)MaxHealth;
        }
    }

    protected void InitiateHealthValues(int maxHp)
    {
        MaxHealth = maxHp;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
        }
        else if (healthImage != null)
        {
            healthImage.fillAmount = 1f;
        }
        UpdateHealthBar();
    }
}
