// Author: Bilal El Medkouri
// Co-Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthImage;
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
        if(healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
        }
        else if(healthImage != null)
        {
            healthImage.fillAmount = 1f;
        }
        UpdateHealthBar();
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
}
