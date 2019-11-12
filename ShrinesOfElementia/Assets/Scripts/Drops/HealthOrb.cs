using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : DroppableObject
{
    [SerializeField] int healthAmount;

    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.Health.CurrentHealth += healthAmount;
        }

        Destroy(gameObject);
    }
}
