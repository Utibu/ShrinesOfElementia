//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeCharge : MonoBehaviour
{
    private float currentStamina;
    [SerializeField] private float maxStamina;
    [SerializeField] private float staminaRegenerationAmount;
    [SerializeField] private int staminaRegenerationDelay;
    private float regenerationCountdown;
    [SerializeField] private bool infiniteStamina;

    private Slider charge;
    public Slider Charge { get { return charge; } set { charge = value; } }

    public float CurrentStamina
    {
        get
        {
            return currentStamina;
        }
        set
        {
            if (value > maxStamina)
            {
                currentStamina = maxStamina;
            }
            else if (value > currentStamina)
            {
                currentStamina = value;
            }
            else if (value < currentStamina)
            {
                if (value <= 0)
                {
                    currentStamina = 0;
                }
                else
                {
                    currentStamina = value;
                }
            }
            //staminaSlider.value = currentStamina;
        }
    }

    private void Start()
    {
        charge = GetComponent<Slider>();
        charge.maxValue = maxStamina;
        charge.value = maxStamina;
    }
}
