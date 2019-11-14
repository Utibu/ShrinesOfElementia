// Author: Bilal El Medkouri

using UnityEngine;

public class DamageEventListener : MonoBehaviour
{

    [SerializeField] private int elementalDamageBonus;
    private int totalDamage;
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
            return;
        }

        EnemyValues values;
        if (damageEvent.TargetGameObject.TryGetComponent<EnemyValues>(out values))
        {
            if (values.ElementalType.Equals("Earth") && damageEvent.DamageType.Equals("Wind"))
            {
                totalDamage = damageEvent.Damage + elementalDamageBonus;
                damageEvent.TargetGameObject.GetComponent<EnemySM>().DisableElite();
            }
            else if (values.ElementalType.Equals("Water") && damageEvent.DamageType.Equals("Earth"))
            {
                totalDamage = damageEvent.Damage + elementalDamageBonus;
                damageEvent.TargetGameObject.GetComponent<EnemySM>().DisableElite();
            }
            else if (values.ElementalType.Equals("Fire") && damageEvent.DamageType.Equals("Water"))
            {
                totalDamage = damageEvent.Damage + elementalDamageBonus;
                damageEvent.TargetGameObject.GetComponent<EnemySM>().DisableElite();
            }
            else if (values.ElementalType.Equals("Wind") && damageEvent.DamageType.Equals("Fire")) // else: this damage is ordinary melee
            {
                totalDamage = damageEvent.Damage + elementalDamageBonus;
                damageEvent.TargetGameObject.GetComponent<EnemySM>().DisableElite();
            }

            damageEvent.TargetGameObject.GetComponent<HealthComponent>().CurrentHealth -= totalDamage;
        }
        
        

        //pushback
        //damageEvent.TargetGameObject.GetComponent<EnemySM>().OnAttacked(damageEvent);
        //damageEvent.TargetGameObject.transform.position += damageEvent.TargetGameObject.transform.forward * -0.4f;

        print(damageEvent.InstigatorGameObject + " has dealt " + damageEvent.Damage + " damage to " + damageEvent.TargetGameObject);
    }
}