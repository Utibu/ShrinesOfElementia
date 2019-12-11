using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinesDamage : MonoBehaviour
{

    [SerializeField] private int vinesDamage;
    private float timeBetweenDamageTicks = 1.5f;
    private bool damageTickReady;
     
    // Start is called before the first frame update
    void Start()
    {
        damageTickReady = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && damageTickReady)
        {
            EventManager.Current.FireEvent(new DamageEvent("vines deal damage to player", vinesDamage, gameObject, other.gameObject));
            damageTickReady = false;
            TimerManager.Current.SetNewTimer(gameObject, timeBetweenDamageTicks, SetDamageTickReady);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && damageTickReady)
        {
            EventManager.Current.FireEvent(new DamageEvent("vines deal damage to player", vinesDamage, gameObject, other.gameObject));
            damageTickReady = false;
            TimerManager.Current.SetNewTimer(gameObject, timeBetweenDamageTicks, SetDamageTickReady);
        }
    }


    private void SetDamageTickReady()
    {
        damageTickReady = true;
    }

   
}
