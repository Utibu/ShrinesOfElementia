// Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarController : MonoBehaviour
{
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Text staminaText;
    private int currentStamina;
    private int maxStamina;
    public int CurrentStamina { get { return currentStamina; } set { currentStamina = value; UpdateStaminaBar(); } }
    public int MaxStamina { get { return maxStamina; } set { maxStamina = value; } }

    private void Start()
    {
        //staminaBar.maxValue = maxStamina;
        UpdateStaminaBar();
    }

    private void UpdateStaminaBar()
    {
        staminaText.text = currentStamina + "/" + maxStamina;
        staminaBar.value = currentStamina;
    }
}
