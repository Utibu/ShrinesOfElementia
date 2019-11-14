//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlade : Ability
{
    //private GameObject[] enemiesHit;
    private List<GameObject> enemiesHit;
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime;

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

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player")) && !enemiesHit.Contains(other.gameObject) && other.gameObject.tag != caster.tag)
        {
            EventManager.Current.FireEvent(new DamageEvent("Wind blade dealt " + damage + "to " + other.gameObject, damage, gameObject, other.gameObject, "Wind"));
            enemiesHit.Add(other.gameObject);
        }
        
    }
}
