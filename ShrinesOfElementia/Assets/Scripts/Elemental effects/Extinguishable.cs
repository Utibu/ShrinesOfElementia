//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguishable : MonoBehaviour
{

    [SerializeField]private float ReviveTime;
    private float timer;
    private bool extinguished;

    //private ParticleSystem fire;

    // Start is called before the first frame update
    void Start()
    {
        extinguished = false;
        //fire = GetComponent<ParticleSystem>();
        EventManager.Instance.RegisterListener<GeyserCastEvent>(Extinguish);

    }

    // Update is called once per frame
    void Update()
    {
        if (extinguished)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                reviveFire();
            }
        }
    }

    private void Extinguish(GeyserCastEvent ev)
    {
       

        if (gameObject != null && Vector3.Distance(this.transform.position, ev.affectedPosition) <= ev.effectRange)
        {
            Debug.Log("FIRE IS EXTINGUISHHDD");
            timer = ReviveTime;
            extinguished = true;
            foreach (ParticleSystem fire in GetComponentsInChildren<ParticleSystem>())
            {
                fire.Stop();
            }

            //if its a firewall, disable collider
            if (gameObject.CompareTag("Firewall"))
            {
                GetComponent<Collider>().enabled = false;
            }

            //if its an enemy, disable casting!
            if (gameObject.CompareTag("Enemy"))
            {
                disableCast();
            }
        }
        
    }


    private void reviveFire()
    {
        extinguished = false;
        timer = ReviveTime;
        foreach (ParticleSystem fire in GetComponentsInChildren<ParticleSystem>())
        {
            fire.Play();
        }

        //if its a firewall, enable collider
        if (gameObject.CompareTag("Firewall"))
        {
            GetComponent<Collider>().enabled = true;
        }
    }

    private void disableCast()
    {
        Debug.Log("enemy fireball cast is now disabled");
        GetComponent<EnemySM>().Elite = false;
        GetComponent<EnemySM>().Transition<Chase_BasicEnemy>();
    }

    /*
    public void OnDestroy()
    {
        EventManager.Current.UnregisterListener<GeyserCastEvent>(Extinguish);
    }
    */
}
