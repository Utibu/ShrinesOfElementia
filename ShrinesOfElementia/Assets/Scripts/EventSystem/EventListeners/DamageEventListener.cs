// Author: Bilal El Medkouri
//Co-authors: Niklas Almqvist, Sofia Kauko, Joakim Ljung

using UnityEngine;

public class DamageEventListener : MonoBehaviour
{

    [SerializeField] private int elementalDamageBonus;
    private int totalDamage;
    private void Start()
    {
        EventManager.Instance.RegisterListener<DamageEvent>(OnDamageEvent);
    }

    private void OnDamageEvent(DamageEvent damageEvent)
    {

        if (damageEvent.TargetGameObject.GetComponent<EnemySM>() != null)
        {
            damageEvent.TargetGameObject.GetComponent<EnemySM>().EnemyAttacked();

            if (damageEvent.TargetGameObject?.GetComponent<ActivateCanvasOnDamage>().CanvasIsActive == false)
            {
                damageEvent.TargetGameObject.GetComponent<ActivateCanvasOnDamage>().ActivateCanvas();
            }
        }

        if (damageEvent.TargetGameObject.CompareTag("Player"))
        {
            damageEvent.TargetGameObject.GetComponent<MovementInput>().SlowDown();
        }



        damageEvent.TargetGameObject.GetComponent<HealthComponent>().CurrentHealth -= damageEvent.Damage;

        print(damageEvent.InstigatorGameObject + " has dealt " + damageEvent.Damage + " damage to " + damageEvent.TargetGameObject);
    }
}