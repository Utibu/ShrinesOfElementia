//Author: Joakim Ljung
//co-author: Sofia Kauko

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpikes : Ability
{
    [SerializeField] private int damage = 30;

    private ParticleSystem particles;
    private BoxCollider boxCollider;
    private ArrayList victims;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider>();
        victims = new ArrayList();
        particles.GetComponent<ParticleSystem>().Play();
        TimerManager.Instance.SetNewTimer(gameObject, 2f, RemoveSpikes);
    }

    protected override void CastAbility() // behöver optimering, helst så detta kan avändas från enemy också. 
    {
        base.CastAbility();
        Quaternion spikesRotation = Quaternion.Euler(-90f, gameObject.transform.rotation.eulerAngles.y, 0f);
        GameObject earthSpikes = Instantiate(AbilityPrefab, caster.transform.position + caster.transform.forward * 2f, spikesRotation);
        earthSpikes.GetComponent<EarthSpikes>().Caster = gameObject;
        print(earthSpikes.GetComponent<EarthSpikes>().Caster);
        //earthSpikes.GetComponent<ParticleSystem>().Play();
        //TimerManager.Current.SetNewTimer(gameObject, 2f, RemoveSpikes);
    }

    public void RemoveSpikes()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player")) && other.gameObject.tag != Caster.tag && !victims.Contains(other.gameObject)) 
        {
            
            //print(caster.name);
            //print(other.gameObject.name);
            //print("trigger particle");
            EventManager.Instance.FireEvent(new DamageEvent("damage dealt", damage, gameObject, other.gameObject));
            EventManager.Instance.FireEvent(new EarthAbilityEvent("earthAbility activated", other.gameObject, gameObject.transform.position, boxCollider.size.magnitude));
            victims.Add(other.gameObject);
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
