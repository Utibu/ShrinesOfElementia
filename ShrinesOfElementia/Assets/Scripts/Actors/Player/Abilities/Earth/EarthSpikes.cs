//Author: Joakim Ljung
//co-author: Sofia Kauko

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpikes : Ability
{
    private ParticleSystem particles;
    private BoxCollider boxCollider;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        /*
        if (particles.IsAlive())
        {
            boxCollider.enabled = true;
        }
        else
        {
            boxCollider.enabled = false;
        }
        */
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player")) && other.gameObject.tag != Caster.tag)
        {
            //print(caster.name);
            //print(other.gameObject.name);
            //print("trigger particle");
            EventManager.Current.FireEvent(new DamageEvent("damage dealt", 20, gameObject, other.gameObject));
            EventManager.Current.FireEvent(new EarthAbilityEvent("earthAbility activated", other.gameObject, gameObject.transform.position, boxCollider.size.magnitude));
        }
    }
    /*
    public override void SetCaster(GameObject obj)
    {
        base.SetCaster(obj);
    }

    public override GameObject GetCaster()
    {
        return base.GetCaster();
    }
    */
}
