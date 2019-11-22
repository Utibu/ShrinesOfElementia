//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSensitive : ElementalWeakness
{
    void Start()
    {
        EventManager.Current.RegisterListener<WindAbilityEvent>(ReactToWind);
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
        EventManager.Current.UnregisterListener<WindAbilityEvent>(ReactToWind);
    }
}
