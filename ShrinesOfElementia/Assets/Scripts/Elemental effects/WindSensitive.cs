//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSensitive : ElementalWeakness
{
    void Start()
    {
        EventManager.Instance.RegisterListener<WindAbilityEvent>(ReactToWind);
    }


    private void ReactToWind(WindAbilityEvent ev)
    {
        if (ev.Target.GetInstanceID().Equals(gameObject.GetInstanceID()))
        {
            Debug.Log(gameObject.name + " reacts to windAbility");
            ReactToElement();

        }

    }

    public void OnDestroy()
    {
        try
        {
            EventManager.Instance.UnregisterListener<WindAbilityEvent>(ReactToWind);

        }
        catch (System.NullReferenceException exeption)
        {
            Debug.Log("Null reference caught: " + exeption.StackTrace);
        }
        
    }
}
