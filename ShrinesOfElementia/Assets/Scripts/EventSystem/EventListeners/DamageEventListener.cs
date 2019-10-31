// Author: Bilal El Medkouri

using UnityEngine;

public class DamageEventListener : MonoBehaviour
{
    private void Start()
    {
        EventSystem.Current.RegisterListener<DamageEvent>(OnDamageEvent);
    }

    private void OnDamageEvent(DamageEvent damageEvent)
    {
        //deal damage
        damageEvent.TargetGameObject.GetComponent<HealthComponent>().CurrentHealth -= damageEvent.Damage;

        //pushback
        //damageEvent.TargetGameObject.GetComponent<EnemySM>().OnAttacked(damageEvent);
        //damageEvent.TargetGameObject.transform.position += damageEvent.TargetGameObject.transform.forward * -0.4f;

        print(damageEvent.InstigatorGameObject + " has dealt " + damageEvent.Damage + " damage to " + damageEvent.TargetGameObject);
    }
}