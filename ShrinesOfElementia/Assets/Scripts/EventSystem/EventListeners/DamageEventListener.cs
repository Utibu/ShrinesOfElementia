// Author: Bilal El Medkouri

public class DamageEventListener : EventListener<DamageEvent>
{
    protected override void OnEvent(DamageEvent damageEvent)
    {
        damageEvent.TargetGameObject.GetComponent<HealthComponent>().CurrentHealth -= damageEvent.Damage;

        print(damageEvent.InstigatorGameObject + " has dealt " + damageEvent.Damage + " damage to " + damageEvent.TargetGameObject);
    }
}