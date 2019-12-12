//Author: Joakim Ljung

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
    [SerializeField] private bool infiniteStamina;

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
        EventManager.Instance.RegisterListener<DodgeEvent>(OnDodge);
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
            CurrentStamina += staminaRegenerationAmount;
        }
    }

    private void OnDodge(DodgeEvent eve)
    {
        if (!infiniteStamina)
        {
            if(currentStamina >= maxStamina)
            {
                currentStamina -= maxStamina / 3;
            }
            /*
            else if (currentStamina >= (maxStamina / 3) * 2)
            {
                currentStamina -= maxStamina / 3;
            }
            */
            else if (currentStamina >= maxStamina / 3)
            {
                print("last dodge");
                currentStamina -= maxStamina / 3;
            }
            regenerationCountdown = 0;
        }
    }

    public bool CanDodge()
    {
        return currentStamina >= maxStamina / 3;
    }

    private void UpdateStaminaBar()
    {
        staminaSlider.value = CurrentStamina;
        //staminaSlider.GetComponentInChildren<Text>().text = Mathf.Floor(currentStamina) + "/" + Mathf.Floor(maxStamina);
    }
}
