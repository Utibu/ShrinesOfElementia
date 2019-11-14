// Author: Bilal El Medkouri

using UnityEngine;

public class DamageEventListener : MonoBehaviour
{

    [SerializeField] private int elementalDamageBonus;
    private void Start()
    {
        EventManager.Current.RegisterListener<DamageEvent>(OnDamageEvent);
    }

    private void OnDamageEvent(DamageEvent damageEvent)
    {
        
        if (damageEvent.DamageType.Equals("Melee"))
        {
            //deal melee damage
            damageEvent.TargetGameObject.GetComponent<HealthComponent>().CurrentHealth -= damageEvent.Damage;
        }
        else if (damageEvent.TargetGameObject.GetComponent<EnemyValues>().ElementalType.Equals("Earth") && damageEvent.DamageType.Equals("Wind"))
        {
            damageEvent.TargetGameObject.GetComponent<HealthComponent>().CurrentHealth -= damageEvent.Damage + elementalDamageBonus;
            damageEvent.TargetGameObject.GetComponent<EnemySM>().DisableElite();
        }
        else if (damageEvent.TargetGameObject.GetComponent<EnemyValues>().ElementalType.Equals("Water") && damageEvent.DamageType.Equals("Earth"))
        {
            damageEvent.TargetGameObject.GetComponent<HealthComponent>().CurrentHealth -= damageEvent.Damage + elementalDamageBonus;
            damageEvent.TargetGameObject.GetComponent<EnemySM>().DisableElite();
        }
        else if (damageEvent.TargetGameObject.GetComponent<EnemyValues>().ElementalType.Equals("Fire") && damageEvent.DamageType.Equals("Water"))
        {
            damageEvent.TargetGameObject.GetComponent<HealthComponent>().CurrentHealth -= damageEvent.Damage + elementalDamageBonus;
            damageEvent.TargetGameObject.GetComponent<EnemySM>().DisableElite();
        }
        else if(damageEvent.TargetGameObject.GetComponent<EnemyValues>().ElementalType.Equals("Wind") && damageEvent.DamageType.Equals("Fire")) // else: this damage is ordinary melee
        {
            damageEvent.TargetGameObject.GetComponent<HealthComponent>().CurrentHealth -= damageEvent.Damage + elementalDamageBonus;
            damageEvent.TargetGameObject.GetComponent<EnemySM>().DisableElite();
        }
        

        //pushback
        //damageEvent.TargetGameObject.GetComponent<EnemySM>().OnAttacked(damageEvent);
        //damageEvent.TargetGameObject.transform.position += damageEvent.TargetGameObject.transform.forward * -0.4f;

        print(damageEvent.InstigatorGameObject + " has dealt " + damageEvent.Damage + " damage to " + damageEvent.TargetGameObject);
    }
}