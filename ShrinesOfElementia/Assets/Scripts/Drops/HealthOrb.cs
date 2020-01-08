//Author: Joakim Ljung
//Co-Author: Niklas Almqvist

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : DroppableObject
{
    [SerializeField] int healthAmount;

    protected override void Start()
    {
        healthAmount = Player.Instance.healthDropsAmount;
    }

    protected override void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.Health.CurrentHealth += healthAmount;
            Player.Instance.GetComponent<PlayerSoundController>().PlayOrbClip();
            Destroy(droppedObject);
        }
    }
    
}
