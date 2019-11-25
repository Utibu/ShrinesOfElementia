//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSensitive : ElementalWeakness
{
    void Start()
    {
        EventManager.Current.RegisterListener<GeyserCastEvent>(ReactToWater);
    }


    private void ReactToWater(GeyserCastEvent ev)
    {

        if (Vector3.Distance(gameObject.transform.position, ev.affectedPosition) < ev.effectRange)
        {
            Debug.Log(gameObject.name + " reacts to windAbility");
            ReactToElement();
        }

    }

    public void OnDestroy()
    {
        try
        {

            EventManager.Current.UnregisterListener<GeyserCastEvent>(ReactToWater);
        }
        catch (System.NullReferenceException exeption)
        {
            Debug.Log("Null reference caught: " + exeption.StackTrace);
        }
    }
}
