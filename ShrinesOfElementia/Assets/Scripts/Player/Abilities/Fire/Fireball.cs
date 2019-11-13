// Author: Bilal El Medkouri
// Co-Author: Joakim Ljung

using UnityEngine;

/// <summary>
/// Handles the fireball prefab
/// </summary>
public class Fireball : Ability
{
    [Header("Temporary attributes")]
    [SerializeField] private int directHitDamage;
    [SerializeField] private int aoeDamage;
    [SerializeField] private float aoeRadius, maxRange;

    private Vector3 spawnPosition, currentPosition;

    private bool hasDealtDamage;

    private void Awake()
    {
        spawnPosition = gameObject.transform.position;
        currentPosition = spawnPosition;

        hasDealtDamage = false;
    }
    private void Update()
    {
        currentPosition = gameObject.transform.position;

        //print(Vector3.Distance(spawnPosition, currentPosition));

        if (Vector3.Distance(spawnPosition, currentPosition) >= maxRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasDealtDamage == true)
            return;

        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")) && collision.gameObject.tag != caster.tag)
        {
            DealDamage(collision.gameObject, directHitDamage);

            hasDealtDamage = true;
        }

        AreaOfEffect();

        Destroy(gameObject);
    }

    private void AreaOfEffect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, aoeRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if ((hitColliders[i].gameObject.CompareTag("Enemy") || hitColliders[i].gameObject.CompareTag("Player")) && hitColliders[i].gameObject.tag != caster.tag)
            {
                DealDamage(hitColliders[i].gameObject, aoeDamage);
            }
            i++;
        }
    }

    private void DealDamage(GameObject damagedGameObject, int damage)
    {
        DamageEvent damageEvent = new DamageEvent(damagedGameObject + " has dealt " + damage + " damage to " + damagedGameObject, damage, gameObject, damagedGameObject);
        EventManager.Current.FireEvent(damageEvent);
    }
}