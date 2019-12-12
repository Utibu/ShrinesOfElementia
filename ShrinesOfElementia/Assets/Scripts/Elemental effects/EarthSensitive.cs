//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSensitive : ElementalWeakness
{
    void Start()
    {
        EventManager.Instance.RegisterListener<EarthAbilityEvent>(ReactToEarth);
    }


    private void ReactToEarth(EarthAbilityEvent ev)
    {
        if (ev.Target.GetInstanceID().Equals(gameObject.GetInstanceID()))
        {
            Debug.Log(gameObject.name + " reacts to earthAbility");
            ReactToElement();

        }

    }

    public void OnDestroy()
    {
        try
        {
            EventManager.Instance.UnregisterListener<EarthAbilityEvent>(ReactToEarth);

        }
        catch (System.NullReferenceException exeption)
        {
            Debug.Log("Null reference caught: " + exeption.StackTrace);
        }
        
    }
}
