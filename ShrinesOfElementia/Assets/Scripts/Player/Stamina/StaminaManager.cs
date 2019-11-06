using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    private float currentStamina;
    [SerializeField] private float maxStamina;
    [SerializeField] private float staminaRegenerationAmount;
    [SerializeField] private int staminaRegenerationDelay;
    private float regenerationCountdown;

    [SerializeField] private Slider staminaSlider;

    public float CurrentStamina {
        get
        {
            return currentStamina;
        }
        set
        {
            if(value > maxStamina)
            {
                currentStamina = maxStamina;
            }
            else if (value > currentStamina)
            {
                print("setting");
                currentStamina = value;
            }
            else if(value < currentStamina)
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
            staminaSlider.value = currentStamina;
        }
    }


    private void Start()
    {
        currentStamina = maxStamina;
        regenerationCountdown = 0;
        EventSystem.Current.RegisterListener<StaminaDrainEvent>(OnStaminaDrain);
        staminaSlider.maxValue = maxStamina;
    }

    private void Update()
    {
        regenerationCountdown += Time.deltaTime;
        if (regenerationCountdown >= staminaRegenerationDelay)
        {
            RegenerateStamina();
        }
        UpdateStaminaBar();
    }

    private void RegenerateStamina()
    {
        if (CurrentStamina < maxStamina)
        {
            if (GetComponent<PlayerInput>().IsBlocking)
            {
                regenerationCountdown = 0;
            }
            else
            {
                CurrentStamina += staminaRegenerationAmount;
            }
        }
    }

    private void OnStaminaDrain(StaminaDrainEvent eve)
    {
        CurrentStamina -= eve.DrainAmount;
        regenerationCountdown = 0;
    }

    private void UpdateStaminaBar()
    {
        staminaSlider.value = CurrentStamina;
        //staminaSlider.GetComponentInChildren<Text>().text = Mathf.Floor(currentStamina) + "/" + Mathf.Floor(maxStamina);
    }
}
