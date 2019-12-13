// Author: Bilal El Medkouri
// Co-Author: Joakim Ljung, Sofia Kauko

using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the fireball prefab
/// </summary>
public class Fireball : Ability
{
    [Header("Temporary attributes")]
    [SerializeField] private int directHitDamage;
    [SerializeField] private int aoeDamage;
    [SerializeField] private float aoeRadius, maxRange;
    private ArrayList affected;
    private GameObject patientZero;


    private Vector3 spawnPosition, currentPosition;

    private bool hasDealtDamage;

    private void Awake()
    {
        spawnPosition = gameObject.transform.position;
        currentPosition = spawnPosition;
        affected = new ArrayList();

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

    protected override void CastAbility()
    {
        base.CastAbility();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasDealtDamage == true)
        {
            Destroy(gameObject);
            return;
        }

        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")) && collision.gameObject.CompareTag(casterTag) == false) //collision.gameObject.tag != caster.tag)
        {
            DealDamage(collision.gameObject, directHitDamage);
            hasDealtDamage = true;
        }

        if (!collision.gameObject.CompareTag("Shield"))
        {
            AreaOfEffect();
        }

        else
        {
            EventManager.Instance.FireEvent(new BlockEvent("fire blocked", 30f));
        }
        

        print("Fireball hit " + collision.gameObject);
        EventManager.Instance.FireEvent(new FireAbilityEvent("fireAbility activated", collision.collider.gameObject, gameObject.transform.position, aoeRadius));

        Destroy(gameObject);
    }

    private void AreaOfEffect()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, aoeRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if ((hitColliders[i].gameObject.CompareTag("Enemy") || hitColliders[i].gameObject.CompareTag("Player")) && hitColliders[i].gameObject.CompareTag(casterTag) == false) //.tag != caster.tag)
            {
                // already affested enemmies or the already hit onw should not be damaged.
                if (!affected.Contains(hitColliders[i].gameObject) && !hitColliders[i].gameObject.Equals(patientZero)) 
                {
                    DealDamage(hitColliders[i].gameObject, aoeDamage);
                    affected.Add(hitColliders[i].gameObject);
                }
            }
            i++;
        }
    }

    private void DealDamage(GameObject damagedGameObject, int damage)
    {
        patientZero = damagedGameObject;
        DamageEvent damageEvent = new DamageEvent(damagedGameObject + " has dealt " + damage + " damage to " + damagedGameObject, damage, gameObject, damagedGameObject);
        EventManager.Instance.FireEvent(damageEvent);
    }
}