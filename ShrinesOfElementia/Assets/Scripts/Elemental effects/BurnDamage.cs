﻿//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDamage : MonoBehaviour
{

    [SerializeField] private int DamagePerTick;
    [SerializeField] private float DisableDuration;
    private float tickTime = 0.5f;
    private float countdown;
    private HealthComponent burnvictim;
    private bool isActive = true;

    public bool IsActive { get => isActive; set => isActive = value; }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Current.RegisterListener<GeyserCastEvent>(PauseBurn);
        countdown = tickTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<HealthComponent>(out burnvictim) && isActive)
        {
            countdown -= Time.deltaTime;
            if(countdown < 0)
            {
                burnvictim.CurrentHealth -= DamagePerTick;
                Debug.Log("burndamage dealt");
                countdown = tickTime;
            }
        }
    }

    private void PauseBurn(GeyserCastEvent ev)
    {
        if (Vector3.Distance(gameObject.transform.position, ev.affectedPosition) < ev.effectRange)
        {
            isActive = false;
            GetComponent<Light>().enabled = false;
            TimerManager.Current.SetNewTimer(gameObject, DisableDuration, EnableBurn);


            // is gameobject is a firegiant, transition it to the enable burn state after the disableDuration is done.
            if(GetComponentInParent<Giant>() != null)
            {
                Debug.Log("BurnDamage script fire animation timer");
                System.Action animateEnableBurn = gameObject.GetComponent<Giant>().Transition<GiantCastAuraState>;
                TimerManager.Current.SetNewTimer(gameObject, DisableDuration - 2f,  animateEnableBurn);
            }
        }
    }

    public void EnableBurn()
    {
        isActive = true;
        GetComponent<Light>().enabled = true;

        
    }

    private void OnDestroy()
    {
        try{
            EventManager.Current.UnregisterListener<GeyserCastEvent>(PauseBurn);
        }
        catch (System.NullReferenceException nullRef)
        {
            Debug.Log("NullReferenceExeption caught: " + nullRef.Data);
        }
    }
}
