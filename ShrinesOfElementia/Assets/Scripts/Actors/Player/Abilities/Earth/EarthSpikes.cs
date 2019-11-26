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

    protected override void CastAbility()
    {
        base.CastAbility();
        Quaternion spikesRotation = Quaternion.Euler(-90f, gameObject.transform.rotation.eulerAngles.y, 0f);
        GameObject earthSpikes = Instantiate(AbilityPrefab, caster.transform.position + caster.transform.forward * 2f, spikesRotation);
        earthSpikes.GetComponent<EarthSpikes>().Caster = gameObject;
        print(earthSpikes.GetComponent<EarthSpikes>().Caster);
        earthSpikes.GetComponent<ParticleSystem>().Play();
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
