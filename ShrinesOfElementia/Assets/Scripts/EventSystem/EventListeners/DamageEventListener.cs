// Author: Bilal El Medkouri

using UnityEngine;

public class DamageEventListener : MonoBehaviour
{
    private void Start()
    {
        EventManager.Current.RegisterListener<DamageEvent>(OnDamageEvent);
    }

    private void OnDamageEvent(DamageEvent damageEvent)
    {
        //deal damage
        damageEvent.TargetGameObject.GetComponent<HealthComponent>().CurrentHealth -= damageEvent.Damage;



        //OBS: Jag kommer styra upp detta helvete till kod så snart jag vet att det faktiskt fungerar, har dock inte bestäms system för detta än
        if (damageEvent.TargetGameObject.name.Equals("WindEliteEnemy") && damageEvent.InstigatorGameObject.name.Equals("Fireball(Clone)"))
        {
            damageEvent.TargetGameObject.GetComponent<EnemySM>().Elite = false;
            damageEvent.TargetGameObject.GetComponent<EnemySM>().Transition<Chase_BasicEnemy>();
        }
        else if (damageEvent.TargetGameObject.name.Equals("EarthEliteEnemy") && damageEvent.InstigatorGameObject.name.Equals("WindBlade(Clone)"))
        {
            damageEvent.TargetGameObject.GetComponent<EnemySM>().Elite = false;
            damageEvent.TargetGameObject.GetComponent<EnemySM>().Transition<Chase_BasicEnemy>();
        }
        else if (damageEvent.TargetGameObject.name.Equals("WaterEliteEnemy") && damageEvent.InstigatorGameObject.name.Equals("EarthSpikes 2(Clone)"))
        {
            damageEvent.TargetGameObject.GetComponent<EnemySM>().Elite = false;
            damageEvent.TargetGameObject.GetComponent<EnemySM>().Transition<Chase_BasicEnemy>();
        }
        else {
            Debug.Log("HFIUEZEFIZUSEG" + damageEvent.InstigatorGameObject.name + " " + damageEvent.TargetGameObject.name);
        }
        

        //pushback
        //damageEvent.TargetGameObject.GetComponent<EnemySM>().OnAttacked(damageEvent);
        //damageEvent.TargetGameObject.transform.position += damageEvent.TargetGameObject.transform.forward * -0.4f;

        print(damageEvent.InstigatorGameObject + " has dealt " + damageEvent.Damage + " damage to " + damageEvent.TargetGameObject);
    }
}