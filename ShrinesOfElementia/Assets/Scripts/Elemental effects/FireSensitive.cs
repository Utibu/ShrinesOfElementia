//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSensitive : ElementalWeakness
{
    
    void Start()
    {
        EventManager.Current.RegisterListener<FireAbilityEvent>(ReactToFire);
    }

    
    private void ReactToFire(FireAbilityEvent ev)
    {
        if (Vector3.Distance(ev.PointOfOrigin, this.gameObject.transform.position) < ev.EffectRange )
        {
            Debug.Log(gameObject.name + " reacts to fireAbility");
            ReactToElement();
            
            
        }

    }

    public void OnDestroy()
    {
        EventManager.Current.UnregisterListener<FireAbilityEvent>(ReactToFire);
    }

}
