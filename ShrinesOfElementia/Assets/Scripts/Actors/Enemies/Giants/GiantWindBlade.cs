//Author: Sofia Chyle Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantWindBlade : Ability

{
    //private GameObject[] enemiesHit;
    private List<GameObject> enemiesHit;
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime;
    [SerializeField] private float effectRange;

    private void Start()
    {
        enemiesHit = new List<GameObject>();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        
        transform.localScale = new Vector3(transform.localScale.x * 1.02f, transform.localScale.y * 1.02f, transform.localScale.z * 1.02f);
    }

    private void OnTriggerEnter(Collider other)
    {

        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player")) && !enemiesHit.Contains(other.gameObject) && other.gameObject.tag != casterTag)
        {
            EventManager.Instance.FireEvent(new DamageEvent("Wind blade dealt " + damage + "to " + other.gameObject, damage, gameObject, other.gameObject, true));
            EventManager.Instance.FireEvent(new WindAbilityEvent("WindAbility activated", other.gameObject, transform.position, effectRange));

            enemiesHit.Add(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Vines") && !enemiesHit.Contains(other.gameObject))
        {
            EventManager.Instance.FireEvent(new WindAbilityEvent("WindAbility activated", other.gameObject, transform.position, effectRange));
        }

        else if (other.gameObject.CompareTag("Shield"))
        {
            EventManager.Instance.FireEvent(new BlockEvent("wind blocked", 30f));
            Destroy(this.gameObject);
        }


    }
}
