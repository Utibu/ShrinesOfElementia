// Author: Bilal El Medkouri
// Co-Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private GameObject respawnLocation;
    [SerializeField] private int damage;


    private void OnTriggerEnter(Collider other)
    {
        print("Entered killzone");
        if (other.gameObject.CompareTag("Player"))
        {
            DealDamage(other.gameObject);
            Respawn(other.gameObject);
        }
    }

    private void DealDamage(GameObject targetGameObject)
    {
        DamageEvent damageEvent = new DamageEvent(targetGameObject + " has dealt " + damage + " damage to " + targetGameObject, damage, gameObject, targetGameObject, true);
        EventManager.Instance.FireEvent(damageEvent);
    }

    private void Respawn(GameObject targetGameObject)
    {
        print("Respawn");
        print("Transform before: " + targetGameObject.transform.position);
        

        if (targetGameObject.CompareTag("Player"))
        {
            EventManager.Instance.FireEvent(new RespawnEvent("player respawning", respawnLocation.transform.position));
            //Player.Instance.MovementInput.MoveTo(respawnLocation.transform.position);
        }
        else
        {
            //targetGameObject.transform.position = respawnLocation.transform.position;
        }

        print("Transform after: " + targetGameObject.transform.position);
    }
}
