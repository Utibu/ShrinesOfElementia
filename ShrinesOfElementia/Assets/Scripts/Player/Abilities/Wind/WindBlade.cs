using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlade : MonoBehaviour
{
    //private GameObject[] enemiesHit;
    private List<GameObject> enemiesHit;
    private int damage;
    [SerializeField] private float lifeTime;

    private void Start()
    {
        
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !enemiesHit.Contains(other.gameObject))
        {
            EventSystem.Current.FireEvent(new DamageEvent("Wind blade dealt " + damage + "to " + other.gameObject, damage, gameObject, other.gameObject));
            enemiesHit.Add(other.gameObject);
        }
        
    }
}
