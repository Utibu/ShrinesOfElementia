//Author: Sofia Chyle Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinesDamage : MonoBehaviour
{

    [SerializeField] private int vinesDamage;
    private float timeBetweenDamageTicks = 1.5f;
    private bool damageTickReady;
    [SerializeField] private float fullSizeX;
    [SerializeField] private float fullSizeZ;

     
    // Start is called before the first frame update
    void Start()
    {
        damageTickReady = true;
        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    private void Update()
    {
        if(gameObject.transform.localScale.x < fullSizeX)
        {
            gameObject.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && damageTickReady)
        {
            EventManager.Instance.FireEvent(new DamageEvent("vines deal damage to player", vinesDamage, gameObject, other.gameObject, true));
            damageTickReady = false;
            TimerManager.Instance.SetNewTimer(gameObject, timeBetweenDamageTicks, SetDamageTickReady);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && damageTickReady)
        {
            EventManager.Instance.FireEvent(new DamageEvent("vines deal damage to player", vinesDamage, gameObject, other.gameObject, true));
            damageTickReady = false;
            TimerManager.Instance.SetNewTimer(gameObject, timeBetweenDamageTicks, SetDamageTickReady);
        }
    }


    private void SetDamageTickReady()
    {
        damageTickReady = true;
    }

   
}
